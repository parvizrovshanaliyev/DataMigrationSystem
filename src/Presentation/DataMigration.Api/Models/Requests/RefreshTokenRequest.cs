using System.ComponentModel.DataAnnotations;

namespace DataMigration.Api.Models.Requests;

public class RefreshTokenRequest
{
    [Required]
    public string AccessToken { get; init; } = null!;

    [Required]
    public string RefreshToken { get; init; } = null!;
} 