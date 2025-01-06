using System.ComponentModel.DataAnnotations;

namespace DataMigration.Api.Models.Requests;

public class VerifyMfaRequest
{
    [Required]
    public Guid UserId { get; init; }

    [Required]
    [StringLength(6, MinimumLength = 6)]
    [RegularExpression("^[0-9]*$")]
    public string Code { get; init; } = null!;

    public bool RememberMe { get; init; }
} 