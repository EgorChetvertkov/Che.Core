using Che.Mail.Messages;

using FluentEmail.Core.Models;

namespace Che.Mail.Services;

public interface IMailService
{
    public Task SendMailWithTemplate<TMessage>(
        MailWithRazorTemplate<TMessage> message,
        List<Attachment> attachments,
        CancellationToken cancellationToken) where TMessage : MessageModel;

    public Task SendMailWithPlainText(
        MailWithPlainText message,
        List<Attachment> attachments,
        CancellationToken cancellationToken);

    public Task<bool> TrySendAllUnsentMails(CancellationToken cancellationToken);
}