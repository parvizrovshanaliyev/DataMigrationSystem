using System;
using DataMigration.Application.Common.Exceptions;

namespace DataMigration.UserManagement.Domain.Exceptions;

/// <summary>
/// Exception thrown when a requested user is not found
/// </summary>
public class UserNotFoundException : NotFoundException
{
    /// <summary>
    /// Initializes a new instance of the UserNotFoundException class
    /// </summary>
    public UserNotFoundException(Guid userId)
        : base($"User with ID {userId} was not found")
    {
    }

    /// <summary>
    /// Initializes a new instance of the UserNotFoundException class
    /// </summary>
    public UserNotFoundException(string email)
        : base($"User with email {email} was not found")
    {
    }

    /// <summary>
    /// Initializes a new instance of the UserNotFoundException class
    /// </summary>
    public UserNotFoundException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
} 