using System;

namespace DataMigrationSystem.Application.Models.Authentication;

public class VerifyMfaSetupRequest
{
    public Guid UserId { get; set; }
    public string Code { get; set; } = null!;
    public string? IpAddress { get; set; }
} 