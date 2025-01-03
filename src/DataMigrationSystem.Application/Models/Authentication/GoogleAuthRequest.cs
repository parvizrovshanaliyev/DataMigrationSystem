namespace DataMigrationSystem.Application.Models.Authentication;

public class GoogleAuthRequest
{
    public string IdToken { get; set; } = null!;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
} 