using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Roles;

public sealed class RoleCreatedEvent : DomainEvent
{
    public Guid RoleId { get; }
    public string Name { get; }
    public string? Description { get; }

    public RoleCreatedEvent(Guid roleId, string name, string? description) : base()
    {
        RoleId = roleId;
        Name = name;
        Description = description;
    }
} 