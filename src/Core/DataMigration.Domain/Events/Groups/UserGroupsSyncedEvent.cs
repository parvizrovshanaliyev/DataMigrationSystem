using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Groups;

public sealed class UserGroupsSyncedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public IReadOnlyList<string> Groups { get; }
    public IReadOnlyList<string> Roles { get; }

    public UserGroupsSyncedEvent(
        Guid userId,
        string email,
        IReadOnlyList<string> groups,
        IReadOnlyList<string> roles)
    {
        UserId = userId;
        Email = email;
        Groups = groups;
        Roles = roles;
    }
} 