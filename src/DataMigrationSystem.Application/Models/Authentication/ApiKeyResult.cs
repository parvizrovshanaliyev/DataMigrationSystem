using System;
using System.Collections.Generic;

namespace DataMigrationSystem.Application.Models.Authentication;

public class ApiKeyResult
{
    public bool Succeeded { get; set; }
    public Guid? ApiKeyId { get; set; }
    public string? Key { get; set; }
    public string? Name { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public IEnumerable<string>? Errors { get; set; }
} 