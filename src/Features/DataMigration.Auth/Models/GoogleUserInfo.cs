namespace DataMigration.Auth.Models;

public record GoogleUserInfo
{
    public string Id { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Picture { get; init; }
    public string? Domain { get; init; }
    public bool EmailVerified { get; init; }
} 