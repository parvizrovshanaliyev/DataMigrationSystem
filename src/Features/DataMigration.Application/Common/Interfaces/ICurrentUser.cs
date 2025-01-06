namespace DataMigration.Application.Common.Interfaces;

public interface ICurrentUser
{
    Guid? Id { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    bool IsWorkspaceUser { get; }
    IReadOnlyList<string> Roles { get; }
} 