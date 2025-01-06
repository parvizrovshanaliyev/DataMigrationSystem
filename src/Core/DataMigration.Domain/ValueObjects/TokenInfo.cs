namespace DataMigration.Domain.ValueObjects;

public record TokenInfo
{
    public string UserId { get; init; }
    public List<string> Roles { get; init; } = new();
    public bool IsWorkspaceToken { get; init; }
    public string? GoogleToken { get; init; }
    public DateTime ExpiresAt { get; init; }
}