using DataMigration.Domain.Common;

namespace DataMigration.Domain.Entities;

public class UserGroup : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public  string Name { get; private set; }
    public  string DisplayName { get; private set; }

    private UserGroup() { } // For EF Core

    public static UserGroup Create(Guid userId, string name, string displayName)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(displayName);

        return new UserGroup
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = name,
            DisplayName = displayName
        };
    }
}