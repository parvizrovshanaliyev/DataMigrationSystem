using System;
using DataMigrationSystem.Domain.Entities;

namespace DataMigrationSystem.Domain.Events;

public class ApiKeyCreatedEvent : DomainEvent
{
    public Guid ApiKeyId { get; }
    public Guid UserId { get; }
    public string KeyName { get; }
    public DateTime ExpiresAt { get; }
    public string? IpAddress { get; }

    public ApiKeyCreatedEvent(ApiKey apiKey, string? ipAddress = null)
    {
        ApiKeyId = apiKey.Id;
        UserId = apiKey.UserId;
        KeyName = apiKey.Name;
        ExpiresAt = apiKey.ExpiresAt;
        IpAddress = ipAddress;
    }
} 