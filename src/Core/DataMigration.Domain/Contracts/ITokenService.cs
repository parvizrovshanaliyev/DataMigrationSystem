using DataMigration.Domain.ValueObjects;

namespace DataMigration.Domain.Contracts;

/// <summary>
/// Provides token management functionality for the domain
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates an access token for a user with specified roles
    /// </summary>
    string GenerateAccessToken(User user, IEnumerable<string> roles);

    /// <summary>
    /// Generates a refresh token
    /// </summary>
    string GenerateRefreshToken();

    /// <summary>
    /// Generates a temporary MFA token
    /// </summary>
    string GenerateMfaToken(User user);

    /// <summary>
    /// Validates and decodes a token
    /// </summary>
    TokenInfo ValidateToken(string token);

    /// <summary>
    /// Validates and decodes an MFA token
    /// </summary>
    TokenInfo ValidateMfaToken(string token);

    /// <summary>
    /// Checks if a token has been blacklisted
    /// </summary>
    Task<bool> IsTokenBlacklistedAsync(string token);

    /// <summary>
    /// Adds a token to the blacklist
    /// </summary>
    Task BlacklistTokenAsync(string token);
} 