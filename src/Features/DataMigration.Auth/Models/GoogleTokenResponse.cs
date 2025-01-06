namespace DataMigration.Auth.Models;

public record GoogleTokenResponse
{
    public string AccessToken { get; init; } = string.Empty;
    public string IdToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public int ExpiresIn { get; init; }
    public string TokenType { get; init; } = string.Empty;
    public string Scope { get; init; } = string.Empty;
} 