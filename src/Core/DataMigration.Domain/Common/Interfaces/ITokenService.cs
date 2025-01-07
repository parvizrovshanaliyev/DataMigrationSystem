using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataMigration.Domain.Common.Models;

namespace DataMigration.Domain.Common.Interfaces;

/// <summary>
/// Core token service interface
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a JWT token
    /// </summary>
    Task<TokenInfo> GenerateTokenAsync(
        Guid userId,
        IEnumerable<string> roles,
        IEnumerable<string> permissions,
        IDictionary<string, string>? customClaims = null);

    /// <summary>
    /// Validates a JWT token
    /// </summary>
    Task<TokenValidationResult> ValidateTokenAsync(string token);

    /// <summary>
    /// Generates a refresh token
    /// </summary>
    Task<string> GenerateRefreshTokenAsync();

    /// <summary>
    /// Validates a refresh token
    /// </summary>
    Task<bool> ValidateRefreshTokenAsync(string refreshToken);

    /// <summary>
    /// Revokes a token
    /// </summary>
    Task RevokeTokenAsync(string token);

    /// <summary>
    /// Checks if a token is revoked
    /// </summary>
    Task<bool> IsTokenRevokedAsync(string token);

    /// <summary>
    /// Gets token information
    /// </summary>
    Task<TokenInfo?> GetTokenInfoAsync(string token);

    /// <summary>
    /// Refreshes an access token using a refresh token
    /// </summary>
    Task<TokenInfo> RefreshTokenAsync(string refreshToken);
} 