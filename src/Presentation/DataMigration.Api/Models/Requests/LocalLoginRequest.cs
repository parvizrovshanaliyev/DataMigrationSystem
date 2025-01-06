using System.ComponentModel.DataAnnotations;

namespace DataMigration.Api.Models.Requests;

public class LocalLoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = null!;

    [Required]
    [MinLength(8)]
    public string Password { get; init; } = null!;

    public bool RememberMe { get; init; }
} 