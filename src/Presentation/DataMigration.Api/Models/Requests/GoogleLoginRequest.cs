using System.ComponentModel.DataAnnotations;

namespace DataMigration.Api.Models.Requests;

public class GoogleLoginRequest
{
    [Required]
    public string IdToken { get; init; } = null!;

    public bool RememberMe { get; init; }
} 