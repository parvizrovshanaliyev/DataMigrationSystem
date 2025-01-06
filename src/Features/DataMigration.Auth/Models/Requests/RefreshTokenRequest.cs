namespace DataMigration.Auth.Models.Requests;

public record RefreshTokenRequest
{
    public string RefreshToken { get; init; } = string.Empty;
} 