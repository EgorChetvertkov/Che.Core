namespace Che.Mail;
/// <summary>
/// Represents the settings for a mail server.
/// </summary>
public sealed class MailServerSettings
{
    /// <summary>
    /// Gets or sets the SMTP server host name.
    /// </summary>
    public string SMPTP { get; set; } = null!;
    /// <summary>
    /// Gets or sets the port number for the mail server.
    /// </summary>
    public int Port { get; set; }
    /// <summary>
    /// Gets or sets the name of the sender for outgoing emails.
    /// </summary>
    public string NameFrom { get; set; } = null!;
    /// <summary>
    /// Gets or sets the login for authentication with the mail server.
    /// </summary>
    public string Login { get; set; } = null!;
    /// <summary>
    /// Gets or sets the password for authentication with the mail server.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Gets the default section name for the mail server settings.
    /// </summary>
    public const string DefaultSectionName = nameof(MailServerSettings);
}
