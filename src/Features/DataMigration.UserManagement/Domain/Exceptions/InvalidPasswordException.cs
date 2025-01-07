using System;
using DataMigration.Application.Common.Exceptions;

namespace DataMigration.UserManagement.Domain.Exceptions;

/// <summary>
/// Exception thrown when a password is invalid
/// </summary>
public class InvalidPasswordException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the InvalidPasswordException class
    /// </summary>
    public InvalidPasswordException()
        : base("The provided password is invalid")
    {
    }

    /// <summary>
    /// Initializes a new instance of the InvalidPasswordException class
    /// </summary>
    public InvalidPasswordException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the InvalidPasswordException class
    /// </summary>
    public InvalidPasswordException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Creates an InvalidPasswordException with specific validation details
    /// </summary>
    public static InvalidPasswordException FromValidationErrors(params string[] errors)
    {
        var exception = new InvalidPasswordException();
        foreach (var error in errors)
        {
            exception.Errors.Add(new ValidationError("Password", error));
        }
        return exception;
    }

    /// <summary>
    /// Creates an InvalidPasswordException for common password validation scenarios
    /// </summary>
    public static InvalidPasswordException ForReason(PasswordValidationError error)
    {
        var message = error switch
        {
            PasswordValidationError.TooShort => "Password must be at least 8 characters long",
            PasswordValidationError.TooLong => "Password cannot exceed 128 characters",
            PasswordValidationError.MissingUppercase => "Password must contain at least one uppercase letter",
            PasswordValidationError.MissingLowercase => "Password must contain at least one lowercase letter",
            PasswordValidationError.MissingDigit => "Password must contain at least one digit",
            PasswordValidationError.MissingSpecialChar => "Password must contain at least one special character",
            PasswordValidationError.CommonPassword => "Password is too common or easily guessable",
            PasswordValidationError.MatchesPersonalInfo => "Password cannot contain personal information",
            _ => "The provided password is invalid"
        };

        return new InvalidPasswordException(message);
    }
}

/// <summary>
/// Represents common password validation error scenarios
/// </summary>
public enum PasswordValidationError
{
    TooShort,
    TooLong,
    MissingUppercase,
    MissingLowercase,
    MissingDigit,
    MissingSpecialChar,
    CommonPassword,
    MatchesPersonalInfo
} 