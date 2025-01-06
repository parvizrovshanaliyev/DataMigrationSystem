namespace DataMigration.Domain.Exceptions;

public class InvalidEmailException : DomainException
{
    public InvalidEmailException(string message) : base(message)
    {
    }

    public static InvalidEmailException Empty() =>
        new("Email cannot be empty");

    public static InvalidEmailException TooLong() =>
        new("Email is too long");

    public static InvalidEmailException InvalidFormat() =>
        new("Invalid email format");
} 