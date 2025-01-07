using System;
using System.Threading.Tasks;

namespace DataMigration.Application.Common.Interfaces;

/// <summary>
/// Provides access to the current user context
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Gets the current user's unique identifier
    /// </summary>
    Guid? Id { get; }

    /// <summary>
    /// Gets the current user's username
    /// </summary>
    string? UserName { get; }

    /// <summary>
    /// Gets the current user's email
    /// </summary>
    string? Email { get; }

    /// <summary>
    /// Checks if the current user is in the specified role
    /// </summary>
    Task<bool> IsInRoleAsync(string role);

    /// <summary>
    /// Checks if the current user is authorized under the specified policy
    /// </summary>
    Task<bool> AuthorizeAsync(string policy);

    /// <summary>
    /// Gets whether the current user is authenticated
    /// </summary>
    bool IsAuthenticated { get; }
} 