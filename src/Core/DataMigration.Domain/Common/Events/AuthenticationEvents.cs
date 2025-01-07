using System;

namespace DataMigration.Domain.Common.Events;

public record UserAuthenticatedEvent : IDomainEvent
{
    public Guid UserId { get; }
    public string Provider { get; }
    public DateTime Timestamp { get; }

    public UserAuthenticatedEvent(Guid userId, string provider)
    {
        UserId = userId;
        Provider = provider;
        Timestamp = DateTime.UtcNow;
    }
}

public record AuthenticationFailedEvent : IDomainEvent
{
    public string Username { get; }
    public string Provider { get; }
    public string Reason { get; }
    public DateTime Timestamp { get; }

    public AuthenticationFailedEvent(string username, string provider, string reason)
    {
        Username = username;
        Provider = provider;
        Reason = reason;
        Timestamp = DateTime.UtcNow;
    }
}

public record TokenGeneratedEvent : IDomainEvent
{
    public Guid UserId { get; }
    public string TokenType { get; }
    public DateTime ExpiresAt { get; }

    public TokenGeneratedEvent(Guid userId, string tokenType, DateTime expiresAt)
    {
        UserId = userId;
        TokenType = tokenType;
        ExpiresAt = expiresAt;
    }
}

public record TokenRevokedEvent : IDomainEvent
{
    public Guid UserId { get; }
    public string TokenType { get; }
    public DateTime RevokedAt { get; }

    public TokenRevokedEvent(Guid userId, string tokenType)
    {
        UserId = userId;
        TokenType = tokenType;
        RevokedAt = DateTime.UtcNow;
    }
} 