namespace DataMigration.Auth.Models.Requests;

public record GoogleLoginRequest
{
    public string Code { get; init; } = string.Empty;
    public string RedirectUri { get; init; } = string.Empty;
    public string CodeVerifier { get; init; } = string.Empty;
} 