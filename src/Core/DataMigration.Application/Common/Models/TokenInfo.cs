namespace DataMigration.Application.Common.Models;

public class TokenInfo
{
    public string UserId { get; init; }
    public List<string> Roles { get; init; } = new();
    public bool IsWorkspaceToken { get; init; }
    public string? GoogleToken { get; init; }
    public DateTime ExpiresAt { get; init; }
}