using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Authentication;

public sealed class UserLoggedInEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Username { get; }

    public UserLoggedInEvent(Guid userId, string username) : base()
    {
        UserId = userId;
        Username = username;
    }
}