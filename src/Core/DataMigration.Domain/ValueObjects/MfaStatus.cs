namespace DataMigration.Domain.ValueObjects;

public record MfaStatus
{
    public bool IsEnabled { get; init; }
    public string? Secret { get; init; }
    public DateTime? EnabledAt { get; init; }
    public List<string> RecoveryCodes { get; init; } = new();
}