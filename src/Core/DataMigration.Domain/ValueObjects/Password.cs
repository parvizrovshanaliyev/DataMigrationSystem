using System.Text.RegularExpressions;
using DataMigration.Domain.Common;
using DataMigration.Domain.Exceptions;

namespace DataMigration.Domain.ValueObjects;

public class Password : ValueObject
{
    public string Value { get; }

    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw InvalidPasswordException.Empty();

        if (password.Length < 8)
            throw InvalidPasswordException.TooShort();

        if (!IsValidPassword(password))
            throw InvalidPasswordException.InvalidFormat();

        return new Password(password);
    }

    private static bool IsValidPassword(string password)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSpecialChar = new Regex(@"[!@#$%^&*(),.?""':{}|<>]+");

        return hasNumber.IsMatch(password) &&
               hasUpperChar.IsMatch(password) &&
               hasLowerChar.IsMatch(password) &&
               hasSpecialChar.IsMatch(password);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Password password) => password.Value;

    public override string ToString() => new('*', Value.Length);
} 