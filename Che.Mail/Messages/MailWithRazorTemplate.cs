using Che.Result;

namespace Che.Mail.Messages;
public sealed class MailWithRazorTemplate<TMessage> where TMessage : MessageModel
{
    public string Subject { get; }
    public string To { get; }
    public string TemplatePath { get; }
    public TMessage Model { get; }

    private MailWithRazorTemplate(string subject, string to, string templatePath, TMessage model)
    {
        Subject = subject;
        To = to;
        TemplatePath = templatePath;
        Model = model;
    }

    public static Result<MailWithRazorTemplate<TMessage>> Create(string subject, EmailAddress to, string templatePath, TMessage model)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            return Result<MailWithRazorTemplate<TMessage>>.Failure(new Error("CreateMailError", "Subject cannot be empty"));
        }

        if (!File.Exists(templatePath))
        {
            return Result<MailWithRazorTemplate<TMessage>>.Failure(new Error("CreateMailError", "Template not found"));
        }

        MailWithRazorTemplate<TMessage> mail = new(subject.Trim(), to.Address, templatePath, model);

        return Result<MailWithRazorTemplate<TMessage>>.Success(mail);
    }
}
