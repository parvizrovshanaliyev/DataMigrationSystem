namespace DataMigrationSystem.Application.Models.Authentication;

public class RegistrationRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
} 