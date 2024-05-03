namespace Che.Mail.Messages;

public sealed record EmailAttachment(string ContentId, string Path, bool IsInLine)
{
    public string CidContent => $"cid:{ContentId}";
}