using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Authentication;

public sealed class MfaEnabledEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Username { get; }
    public string Secret { get; }

    public MfaEnabledEvent(Guid userId, string username, string secret) : base()
    {
        UserId = userId;
        Username = username;
        Secret = secret;
    }
}