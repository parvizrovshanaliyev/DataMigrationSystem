namespace DataMigrationSystem.Application.Models.Authentication;

public class MfaVerificationRequest
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string MfaToken { get; set; } = null!;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
} 