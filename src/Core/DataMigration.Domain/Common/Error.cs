namespace DataMigration.Domain.Common;

public sealed class Error : ValueObject
{
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    private Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error NotFound(string message = "Not found") =>
        new("NotFound", message, ErrorType.NotFound);

    public static Error Validation(string message) =>
        new("Validation", message, ErrorType.Validation);

    public static Error Conflict(string message) =>
        new("Conflict", message, ErrorType.Conflict);

    public static Error Unauthorized(string message = "Unauthorized") =>
        new("Unauthorized", message, ErrorType.Unauthorized);

    public static Error Forbidden(string message = "Forbidden") =>
        new("Forbidden", message, ErrorType.Forbidden);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return Message;
        yield return Type;
    }
}

public enum ErrorType
{
    NotFound,
    Validation,
    Conflict,
    Unauthorized,
    Forbidden
} 