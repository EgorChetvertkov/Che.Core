using System.Diagnostics.CodeAnalysis;

namespace Che.Result;
public sealed class Result<T>
    where T : class
{
    public T? Value { get; }
    public Error Error { get; }
    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess => Error.IsNone;
    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsFailure => !IsSuccess;

    private Result(T? value, Error error)
    {
        if (value is null && error.IsNone)
        {
            throw new ArgumentException("Value and Error cannot both be null");
        }

        if (value is not null && !error.IsNone)
        {
            throw new ArgumentException("Value and Error cannot both be non-null");
        }

        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value, Error.None);
    public static Result<T> Failure(Error error) => new(null, error);
}
