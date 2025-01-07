using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMigration.Domain.Common.Interfaces;

/// <summary>
/// Core authorization service interface
/// </summary>
public interface IAuthorizationService
{
    /// <summary>
    /// Checks if a user has permission to perform an action on a resource
    /// </summary>
    Task<bool> AuthorizeAsync(Guid userId, string resource, string action);

    /// <summary>
    /// Gets all permissions for a user
    /// </summary>
    Task<IEnumerable<string>> GetPermissionsAsync(Guid userId);

    /// <summary>
    /// Checks if a user is in a role
    /// </summary>
    Task<bool> IsInRoleAsync(Guid userId, string role);

    /// <summary>
    /// Gets all roles for a user
    /// </summary>
    Task<IEnumerable<string>> GetRolesAsync(Guid userId);

    /// <summary>
    /// Assigns a role to a user
    /// </summary>
    Task AssignRoleAsync(Guid userId, string role);

    /// <summary>
    /// Removes a role from a user
    /// </summary>
    Task RemoveRoleAsync(Guid userId, string role);

    /// <summary>
    /// Validates a policy for a user
    /// </summary>
    Task<bool> ValidatePolicyAsync(Guid userId, string policy);

    /// <summary>
    /// Gets all policies that apply to a user
    /// </summary>
    Task<IEnumerable<string>> GetPoliciesAsync(Guid userId);
} 