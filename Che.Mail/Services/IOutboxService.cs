using FluentEmail.Core.Models;

namespace Che.Mail.Services;
public interface IOutboxService
{
    Task<Ulid> CreateMail(string to, string subject, string body, bool isHtml, List<Attachment> attachments, CancellationToken cancellationToken);
    Task<List<MailModel>> GetUnsentMailsAsync(CancellationToken cancellationToken);
    Task MarkAsSendAsync(Ulid outboxEntityId, CancellationToken cancellationToken);
}
