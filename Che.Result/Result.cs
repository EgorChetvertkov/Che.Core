using Che.Result.Exceptions;

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
            throw new BadResultException("It is impossible not to fill in the error and the object at the same time");
        }

        if (value is not null && !error.IsNone)
        {
            throw new BadResultException("The result cannot contain both an error and an object at the same time");
        }

        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value, Error.None);
    public static Result<T> Failure(Error error) => new(null, error);
}
