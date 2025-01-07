using System.Collections.Generic;

namespace DataMigration.Domain.Common;

/// <summary>
/// Represents the result of a domain operation
/// </summary>
/// <typeparam name="T">The type of the result value</typeparam>
public class Result<T>
{
    private readonly List<string> _errors = new();
    
    /// <summary>
    /// Gets the value of the result
    /// </summary>
    public T Value { get; }
    
    /// <summary>
    /// Gets whether the operation was successful
    /// </summary>
    public bool IsSuccess => _errors.Count == 0;
    
    /// <summary>
    /// Gets whether the operation failed
    /// </summary>
    public bool IsFailure => !IsSuccess;
    
    /// <summary>
    /// Gets the error messages if the operation failed
    /// </summary>
    public IReadOnlyList<string> Errors => _errors.AsReadOnly();

    private Result(T value)
    {
        Value = value;
    }

    private Result(string error)
    {
        _errors.Add(error);
        Value = default!;
    }

    private Result(IEnumerable<string> errors)
    {
        _errors.AddRange(errors);
        Value = default!;
    }

    /// <summary>
    /// Creates a successful result with the specified value
    /// </summary>
    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    /// <summary>
    /// Creates a failed result with the specified error
    /// </summary>
    public static Result<T> Failure(string error)
    {
        return new Result<T>(error);
    }

    /// <summary>
    /// Creates a failed result with the specified errors
    /// </summary>
    public static Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(errors);
    }

    /// <summary>
    /// Combines multiple results into a single result
    /// </summary>
    public static Result<IEnumerable<T>> Combine(params Result<T>[] results)
    {
        var errors = new List<string>();
        var values = new List<T>();

        foreach (var result in results)
        {
            if (result.IsFailure)
            {
                errors.AddRange(result.Errors);
            }
            else
            {
                values.Add(result.Value);
            }
        }

        return errors.Count > 0
            ? Failure<IEnumerable<T>>(errors)
            : Success<IEnumerable<T>>(values);
    }
}

/// <summary>
/// Provides static methods for creating Results
/// </summary>
public static class Result
{
    /// <summary>
    /// Creates a successful result with the specified value
    /// </summary>
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    /// <summary>
    /// Creates a failed result with the specified error
    /// </summary>
    public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);

    /// <summary>
    /// Creates a failed result with the specified errors
    /// </summary>
    public static Result<T> Failure<T>(IEnumerable<string> errors) => Result<T>.Failure(errors);
} 