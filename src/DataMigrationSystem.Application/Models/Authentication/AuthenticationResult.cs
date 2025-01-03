using System;
using System.Collections.Generic;

namespace DataMigrationSystem.Application.Models.Authentication;

public class AuthenticationResult
{
    public bool Succeeded { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool RequiresMfa { get; set; }
    public string? MfaToken { get; set; }
    public IEnumerable<string>? Errors { get; set; }
    public string? Email { get; set; }
    public Guid? UserId { get; set; }
    public IEnumerable<string>? Roles { get; set; }
} 