namespace DataMigration.Auth.Models;

public record TokenInfo
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public List<string> Roles { get; init; } = new();
    public bool IsWorkspaceToken { get; init; }
    public string? GoogleToken { get; init; }
    public DateTime ExpiresAt { get; init; }
} 