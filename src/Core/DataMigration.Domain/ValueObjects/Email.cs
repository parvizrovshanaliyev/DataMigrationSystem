using System.Text.RegularExpressions;
using DataMigration.Domain.Common;
using DataMigration.Domain.Exceptions;

namespace DataMigration.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email? Create(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw InvalidEmailException.Empty();

        email = email.Trim();
        if (email.Length > 256)
            throw InvalidEmailException.TooLong();

        if (!IsValidEmail(email))
            throw InvalidEmailException.InvalidFormat();

        return new Email(email);
    }

    private static bool IsValidEmail(string email)
    {
        // Basic email validation
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public static implicit operator string(Email email) => email.Value;

    public override string ToString() => Value;
} 