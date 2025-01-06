namespace DataMigration.Auth.Models;

public record AuthResponse
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public int ExpiresIn { get; init; }
    public bool RequiresMfa { get; init; }
    public string? MfaToken { get; init; }
    public UserResponse? User { get; init; }
} 