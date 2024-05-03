using Che.Result;

namespace Che.Mail.Messages;
public sealed class MailWithPlainText
{
    public string Subject { get; }
    public string To { get; }
    public string TemplatePath { get; }
    public string Body { get; }

    private MailWithPlainText(string subject, string to, string templatePath, string body)
    {
        Subject = subject;
        To = to;
        TemplatePath = templatePath;
        Body = body;
    }

    public static Result<MailWithPlainText> Create(string subject, EmailAddress to, string templatePath, string body)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            return Result<MailWithPlainText>.Failure(new Error("CreateMailError", "Subject cannot be empty"));
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            return Result<MailWithPlainText>.Failure(new Error("CreateMailError", "Body cannot be empty"));
        }

        if (!File.Exists(templatePath))
        {
            return Result<MailWithPlainText>.Failure(new Error("CreateMailError", "Template not found"));
        }

        MailWithPlainText mail = new(subject.Trim(), to.Address, templatePath, body.Trim());

        return Result<MailWithPlainText>.Success(mail);
    }
}
