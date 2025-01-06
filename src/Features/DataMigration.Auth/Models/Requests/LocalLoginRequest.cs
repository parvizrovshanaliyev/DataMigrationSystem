namespace DataMigration.Auth.Models.Requests;

public record LocalLoginRequest
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool RememberMe { get; init; }
} 