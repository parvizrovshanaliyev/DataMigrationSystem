using System;

namespace DataMigrationSystem.Application.Models.Authentication;

public class CreateApiKeyRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? ExpiresAt { get; set; }
    public string? IpAddress { get; set; }
} 