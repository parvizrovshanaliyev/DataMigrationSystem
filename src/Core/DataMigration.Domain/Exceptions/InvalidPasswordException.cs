namespace DataMigration.Domain.Exceptions;

public class InvalidPasswordException : DomainException
{
    public InvalidPasswordException(string message) : base(message)
    {
    }

    public static InvalidPasswordException Empty() =>
        new("Password cannot be empty");

    public static InvalidPasswordException TooShort() =>
        new("Password must be at least 8 characters long");

    public static InvalidPasswordException InvalidFormat() =>
        new("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character");
} 