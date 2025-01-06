using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Profile;

public sealed class UserProfileUpdatedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Name { get; }
    public string Email { get; }
    public string? Picture { get; }

    public UserProfileUpdatedEvent(
        Guid userId,
        string name,
        string email,
        string? picture = null)
    {
        UserId = userId;
        Name = name;
        Email = email;
        Picture = picture;
    }
} 