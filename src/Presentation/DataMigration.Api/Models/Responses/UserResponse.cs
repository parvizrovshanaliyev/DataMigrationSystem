namespace DataMigration.Api.Models.Responses;

public class UserResponse
{
    public Guid Id { get; init; }
    public string? Email { get; init; }
    public string? Name { get; init; }
    public string? Picture { get; init; }
    public List<string> Roles { get; init; } = new();
    public bool IsMfaEnabled { get; init; }
    public bool IsEmailVerified { get; init; }
    public bool IsWorkspaceUser { get; init; }
    public string? Domain { get; init; }
} 