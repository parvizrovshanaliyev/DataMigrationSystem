using System;

namespace DataMigration.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when a user attempts to access a resource they are not authorized to access
/// </summary>
public class ForbiddenAccessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the ForbiddenAccessException class
    /// </summary>
    public ForbiddenAccessException()
        : base("Access to this resource is forbidden")
    {
    }

    /// <summary>
    /// Initializes a new instance of the ForbiddenAccessException class with a specified error message
    /// </summary>
    public ForbiddenAccessException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the ForbiddenAccessException class with a specified error message 
    /// and a reference to the inner exception that is the cause of this exception
    /// </summary>
    public ForbiddenAccessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
} 