using Che.Result;

using System.Text.RegularExpressions;

namespace Che.Mail;
public sealed partial class EmailAddress
{
    public string Address { get; }

    private EmailAddress(string address)
    {
        Address = address;
    }

    public static Result<EmailAddress> Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return Result<EmailAddress>.Failure(new Error("CreateMailError", "Address cannot be empty"));
        }

        if (!EmailAddressValidator().IsMatch(address))
        {
            return Result<EmailAddress>.Failure(new Error("CreateMailError", "Address is not valid"));
        }

        EmailAddress email = new(address);
        return Result<EmailAddress>.Success(email);
    }

    public override string ToString()
    {
        return Address; 
    }

    public override bool Equals(object? obj)
    {
        return obj is EmailAddress other && other.Address == Address;
    }

    public override int GetHashCode()
    {
        return Address.GetHashCode();
    }

    [GeneratedRegex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$")]
    private static partial Regex EmailAddressValidator();
}
