namespace DataMigrationSystem.Application.Models.Authentication;

public class LoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? MfaCode { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
} 