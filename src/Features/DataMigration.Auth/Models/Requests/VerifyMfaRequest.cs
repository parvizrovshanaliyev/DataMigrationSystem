namespace DataMigration.Auth.Models.Requests;

public record VerifyMfaRequest
{
    public string MfaToken { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
} 