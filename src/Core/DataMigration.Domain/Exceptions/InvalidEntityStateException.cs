namespace DataMigration.Domain.Exceptions;

public class InvalidEntityStateException : DomainException
{
    public InvalidEntityStateException(string message) : base(message)
    {
    }
}