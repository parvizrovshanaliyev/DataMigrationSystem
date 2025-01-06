namespace DataMigration.Application.Auth.Models;

public record UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Picture { get; init; }
    public bool IsWorkspaceUser { get; init; }
    public string? Domain { get; init; }
    public bool IsMfaEnabled { get; init; }
    public List<string> Roles { get; init; } = new();
} 