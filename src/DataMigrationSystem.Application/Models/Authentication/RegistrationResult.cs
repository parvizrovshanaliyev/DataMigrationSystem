using System;
using System.Collections.Generic;

namespace DataMigrationSystem.Application.Models.Authentication;

public class RegistrationResult
{
    public bool Succeeded { get; set; }
    public Guid? UserId { get; set; }
    public string? Email { get; set; }
    public bool RequiresEmailVerification { get; set; }
    public IEnumerable<string>? Errors { get; set; }
} 