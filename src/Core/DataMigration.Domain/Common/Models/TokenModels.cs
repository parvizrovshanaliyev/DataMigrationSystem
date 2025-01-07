using System;
using System.Collections.Generic;

namespace DataMigration.Domain.Common.Models;

/// <summary>
/// Represents token information
/// </summary>
public record TokenInfo
{
    /// <summary>
    /// Gets the access token
    /// </summary>
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// Gets the refresh token
    /// </summary>
    public string RefreshToken { get; init; } = string.Empty;

    /// <summary>
    /// Gets the token type
    /// </summary>
    public string TokenType { get; init; } = "Bearer";

    /// <summary>
    /// Gets the expiration time in seconds
    /// </summary>
    public int ExpiresIn { get; init; }

    /// <summary>
    /// Gets the user ID associated with the token
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the roles associated with the token
    /// </summary>
    public IEnumerable<string> Roles { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Gets the permissions associated with the token
    /// </summary>
    public IEnumerable<string> Permissions { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Gets any custom claims associated with the token
    /// </summary>
    public IDictionary<string, string> CustomClaims { get; init; } = new Dictionary<string, string>();
}

/// <summary>
/// Represents token validation result
/// </summary>
public record TokenValidationResult
{
    /// <summary>
    /// Gets whether the token is valid
    /// </summary>
    public bool IsValid { get; init; }

    /// <summary>
    /// Gets the validation error if any
    /// </summary>
    public string? Error { get; init; }

    /// <summary>
    /// Gets the token information if valid
    /// </summary>
    public TokenInfo? TokenInfo { get; init; }

    /// <summary>
    /// Creates a successful validation result
    /// </summary>
    public static TokenValidationResult Success(TokenInfo tokenInfo) => new()
    {
        IsValid = true,
        TokenInfo = tokenInfo
    };

    /// <summary>
    /// Creates a failed validation result
    /// </summary>
    public static TokenValidationResult Failure(string error) => new()
    {
        IsValid = false,
        Error = error
    };
} 