using System.Collections.ObjectModel;

namespace Che.Mail.Services;
public sealed class MailModel
{
    public Ulid Id { get; }
    public string To { get; }
    public string Subject { get; }
    public string Body { get;  }
    public bool IsHtml { get;  }
    public ReadOnlyCollection<MailAttachment> Attachments { get; }

    public MailModel(Ulid id, string to, string subject, string body, bool isHtml, ReadOnlyCollection<MailAttachment> attachments)
    {
        Id = id;
        To = to;
        Subject = subject;
        Body = body;
        IsHtml = isHtml;
        Attachments = attachments;
    }
}
