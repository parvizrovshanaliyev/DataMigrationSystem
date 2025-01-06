using DataMigration.Domain.ValueObjects;

namespace DataMigration.Domain.Contracts;

/// <summary>
/// Provides workspace integration functionality for the domain
/// </summary>
public interface IWorkspaceService
{
    /// <summary>
    /// Gets user information from the workspace provider
    /// </summary>
    Task<GoogleUserInfo> GetUserInfoAsync(string token);

    /// <summary>
    /// Gets user's group memberships
    /// </summary>
    Task<List<string>> GetUserGroupsAsync(string email);

    /// <summary>
    /// Validates a workspace access token
    /// </summary>
    Task<bool> ValidateTokenAsync(string token);

    /// <summary>
    /// Validates if a user has access to a domain
    /// </summary>
    Task<bool> ValidateDomainAccessAsync(string email, string domain);

    /// <summary>
    /// Synchronizes user's group memberships
    /// </summary>
    Task SyncUserGroupsAsync(string email);

    /// <summary>
    /// Checks if a user is a workspace user
    /// </summary>
    Task<bool> IsWorkspaceUserAsync(string email);
} 