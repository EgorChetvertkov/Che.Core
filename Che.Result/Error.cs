namespace Che.Result;

public sealed record Error(string Type, string Message)
{
    public static Error None => new(string.Empty, string.Empty);
    public bool IsNone => this == Error.None;
}