namespace DataMigrationSystem.Application.Models.Authentication;

public class ResetPasswordRequest
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
} 