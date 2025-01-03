using System.Collections.Generic;

namespace DataMigrationSystem.Application.Models.Authentication;

public class MfaSetupResult
{
    public bool Succeeded { get; set; }
    public string? SecretKey { get; set; }
    public string? QrCodeUri { get; set; }
    public IEnumerable<string>? RecoveryCodes { get; set; }
    public IEnumerable<string>? Errors { get; set; }
} 