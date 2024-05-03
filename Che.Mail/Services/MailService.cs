using Che.Mail.Messages;

using FluentEmail.Core;
using FluentEmail.Core.Models;

using Microsoft.Extensions.Logging;

namespace Che.Mail.Services;
public sealed class MailService(
    IFluentEmail fluentEmail,
    IOutboxService outbox,
    ILogger<MailService> logger) : IMailService
{
    public const byte MaxRetries = 3;

    public async Task SendMailWithPlainText(MailWithPlainText message, List<Attachment> attachments, CancellationToken cancellationToken)
    {
        fluentEmail.To(message.To).Subject(message.Subject)
            .Body(message.Body, false);
        
        Task<Ulid> saveMailTask = outbox.CreateMail(message.To, message.Subject, fluentEmail.Data.Body, false, attachments, cancellationToken);

        if (attachments is not null && attachments.Count > 0)
        {
            fluentEmail.Attach(attachments);
        }

        Ulid outboxEntityId = await saveMailTask;
        await TrySend(fluentEmail, outbox, logger, outboxEntityId, cancellationToken);
    }

    public async Task SendMailWithTemplate<TMessage>(MailWithRazorTemplate<TMessage> message, List<Attachment> attachments, CancellationToken cancellationToken) where TMessage : MessageModel
    {
        fluentEmail.To(message.To).Subject(message.Subject)
            .UsingTemplateFromFile(message.TemplatePath, message.Model);

        attachments ??= [];

        foreach (var item in message.Model.InLineAttachments.Values)
        {
            Attachment attachment = new()
            {
                Data = new FileStream(item.Path, FileMode.Open, FileAccess.Read, FileShare.Read),
                ContentId = item.ContentId,
                IsInline = true,
                Filename = item.Path,
            };

            attachments.Add(attachment);
        }

        Task<Ulid> saveMailTask = outbox.CreateMail(message.To, message.Subject, fluentEmail.Data.Body, false, attachments, cancellationToken);

        if (attachments is not null && attachments.Count > 0)
        {
            fluentEmail.Attach(attachments);
        }

        Ulid outboxEntityId = await saveMailTask;
        await TrySend(fluentEmail, outbox, logger, outboxEntityId, cancellationToken);
    }

    public async Task<bool> TrySendAllUnsentMails(CancellationToken cancellationToken)
    {
        List<MailModel> mails = await outbox.GetUnsentMailsAsync(cancellationToken);
        foreach (var mail in mails)
        {
            fluentEmail.To(mail.To).Subject(mail.Subject).Body(mail.Body, mail.IsHtml);

            foreach (var item in mail.Attachments)
            {
                fluentEmail.Attach(new Attachment()
                {
                    Data = new FileStream(item.ContentPath, FileMode.Open, FileAccess.Read, FileShare.Read),
                    ContentId = item.ContentId,
                    IsInline = item.IsInLine,
                    Filename = item.ContentPath,
                });
            }

            await TrySend(fluentEmail, outbox, logger, mail.Id, cancellationToken);
        }

        return true;
    }

    private static async Task TrySend(
        IFluentEmail fluentEmail,
        IOutboxService outbox,
        ILogger<MailService> logger,
        Ulid outboxEntityId,
        CancellationToken cancellationToken)
    {
        int attempts = 0;
        while (attempts < MaxRetries)
        {
            try
            {
                await fluentEmail.SendAsync(cancellationToken);

                await outbox.MarkAsSendAsync(outboxEntityId, cancellationToken);

                break;
            }
            catch (Exception ex)
            {
                attempts++;
                if (attempts == MaxRetries)
                {
                    logger.LogError(ex, "[MAIL ERR]");
                }
            }
        }
    }
}
