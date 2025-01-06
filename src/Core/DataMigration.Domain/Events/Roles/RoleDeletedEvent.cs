using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Roles;

public sealed class RoleDeletedEvent : DomainEvent
{
    public Guid RoleId { get; }
    public string Name { get; }

    public RoleDeletedEvent(Guid roleId, string name)
    {
        RoleId = roleId;
        Name = name;
    }
} 