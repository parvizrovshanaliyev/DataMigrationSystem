using DataMigration.Domain.Common;
using DataMigration.Domain.Events.Roles;
using DataMigration.Domain.ValueObjects;

namespace DataMigration.Domain.Entities;

public class Role : AggregateRoot
{
    public Guid Id { get; private set; }
    public  string Name { get; private set; }
    public string? Description { get; private set; }
    private readonly List<Permission> _permissions = new();
    private readonly List<string> _allowedDomains = new();

    public IReadOnlyList<Permission> Permissions => _permissions.AsReadOnly();
    public IReadOnlyList<string> AllowedDomains => _allowedDomains.AsReadOnly();

    private Role() { } // For EF Core

    public static Role Create(string name, string? description)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        var role = new Role();
        role.Apply(new RoleCreatedEvent(Guid.NewGuid(), name, description));
        return role;
    }

    public void Update(string name, string? description)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Apply(new RoleUpdatedEvent(Id, name, description));
    }

    public void Delete()
    {
        Apply(new RoleDeletedEvent(Id, Name));
    }

    public void AddPermission(Permission permission)
    {
        ArgumentNullException.ThrowIfNull(permission);

        if (!_permissions.Contains(permission))
        {
            Apply(new PermissionAddedToRoleEvent(Id, permission));
        }
    }

    public void RemovePermission(Permission permission)
    {
        ArgumentNullException.ThrowIfNull(permission);

        if (_permissions.Contains(permission))
        {
            Apply(new PermissionRemovedFromRoleEvent(Id, permission));
        }
    }

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case RoleCreatedEvent e:
                Id = e.RoleId;
                Name = e.Name;
                Description = e.Description;
                break;

            case RoleUpdatedEvent e:
                Name = e.Name;
                Description = e.Description;
                break;

            case PermissionAddedToRoleEvent e:
                _permissions.Add(e.Permission);
                break;

            case PermissionRemovedFromRoleEvent e:
                _permissions.Remove(e.Permission);
                break;
        }
    }
}