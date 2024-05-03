namespace Che.Mail.Services;

public class MailAttachment
{
    public string ContentId { get; }
    public string ContentPath { get; }
    public bool IsInLine { get; }

    public MailAttachment(string contentId, string contentPath, bool isInLine)
    {
        ContentId = contentId;
        ContentPath = contentPath;
        IsInLine = isInLine;
    }
}