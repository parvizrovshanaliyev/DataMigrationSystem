using System;

namespace DataMigration.Domain.Common;

/// <summary>
/// Guard clauses for domain validation
/// </summary>
public static class Guard
{
    /// <summary>
    /// Ensures that a string is not empty.
    /// </summary>
    public static void AgainstEmptyString(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{parameterName} cannot be empty.", parameterName);
        }
    }

    /// <summary>
    /// Ensures that a value is not null.
    /// </summary>
    public static void AgainstNull<T>(T value, string parameterName) where T : class
    {
        if (value is null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    /// <summary>
    /// Ensures that a Guid is not empty.
    /// </summary>
    public static void AgainstEmptyGuid(Guid value, string parameterName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException($"{parameterName} cannot be empty.", parameterName);
        }
    }

    /// <summary>
    /// Ensures that a number is positive.
    /// </summary>
    public static void AgainstNegativeOrZero(decimal value, string parameterName)
    {
        if (value <= 0)
        {
            throw new ArgumentException($"{parameterName} must be positive.", parameterName);
        }
    }

    /// <summary>
    /// Ensures that a string length is within specified range.
    /// </summary>
    public static void StringLength(string value, string parameterName, int minLength, int maxLength)
    {
        if (value.Length < minLength || value.Length > maxLength)
        {
            throw new ArgumentException(
                $"{parameterName} must be between {minLength} and {maxLength} characters.", 
                parameterName);
        }
    }

    /// <summary>
    /// Ensures that a value is within a specified range.
    /// </summary>
    public static void WithinRange<T>(T value, string parameterName, T min, T max) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
        {
            throw new ArgumentException(
                $"{parameterName} must be between {min} and {max}.", 
                parameterName);
        }
    }

    /// <summary>
    /// Ensures that a condition is true.
    /// </summary>
    public static void IsTrue(bool condition, string message)
    {
        if (!condition)
        {
            throw new ArgumentException(message);
        }
    }
} 