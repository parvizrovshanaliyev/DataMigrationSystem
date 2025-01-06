namespace DataMigration.Domain.Contracts;

/// <summary>
/// Provides Multi-Factor Authentication functionality for the domain
/// </summary>
public interface IMfaService
{
    /// <summary>
    /// Generates a new MFA secret
    /// </summary>
    string GenerateSecret();

    /// <summary>
    /// Generates a TOTP code for a given secret
    /// </summary>
    string GenerateCode(string secret);

    /// <summary>
    /// Validates a TOTP code against a secret
    /// </summary>
    bool ValidateCode(string secret, string code);

    /// <summary>
    /// Generates recovery codes for backup authentication
    /// </summary>
    List<string> GenerateRecoveryCodes(int count = 8);

    /// <summary>
    /// Gets the QR code URI for MFA setup
    /// </summary>
    string GetQrCodeUri(string email, string secret, string issuer = "DataMigration");
} 