using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Roles;

public sealed class RoleUpdatedEvent : DomainEvent
{
    public Guid RoleId { get; }
    public string Name { get; }
    public string? Description { get; }

    public RoleUpdatedEvent(Guid roleId, string name, string? description) : base()
    {
        RoleId = roleId;
        Name = name;
        Description = description;
    }
} 