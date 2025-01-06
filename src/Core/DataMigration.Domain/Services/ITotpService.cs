namespace DataMigration.Domain.Services;

public interface ITotpService
{
    Task<string> GenerateSecretAsync();
    Task<string> GenerateQrCodeUriAsync(string email, string secret);
    Task<bool> ValidateCodeAsync(string secret, string code);
} 