using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Profile;

public sealed class UserCreatedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string Username { get; }
    public string Name { get; }

    public UserCreatedEvent(Guid userId, string email, string username, string name) : base()
    {
        UserId = userId;
        Email = email;
        Username = username;
        Name = name;
    }
}