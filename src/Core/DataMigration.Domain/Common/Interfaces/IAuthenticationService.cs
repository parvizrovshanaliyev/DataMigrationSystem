using System;
using System.Threading.Tasks;

namespace DataMigration.Domain.Common.Interfaces;

/// <summary>
/// Core authentication service interface
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Validates user credentials
    /// </summary>
    Task<bool> ValidateCredentialsAsync(string username, string password);

    /// <summary>
    /// Validates a token
    /// </summary>
    Task<bool> ValidateTokenAsync(string token);

    /// <summary>
    /// Generates an access token
    /// </summary>
    Task<string> GenerateAccessTokenAsync(Guid userId, string[] roles);

    /// <summary>
    /// Generates a refresh token
    /// </summary>
    Task<string> GenerateRefreshTokenAsync(Guid userId);

    /// <summary>
    /// Revokes a token
    /// </summary>
    Task RevokeTokenAsync(string token);

    /// <summary>
    /// Refreshes an access token using a refresh token
    /// </summary>
    Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// Validates MFA code
    /// </summary>
    Task<bool> ValidateMfaCodeAsync(Guid userId, string code);

    /// <summary>
    /// Generates MFA secret
    /// </summary>
    Task<string> GenerateMfaSecretAsync(Guid userId);
} 