namespace Che.Mail.Messages;
public abstract class MessageModel(Dictionary<string, EmailAttachment> inLineAttachments)
{
    public IReadOnlyDictionary<string, EmailAttachment> InLineAttachments => inLineAttachments;
}