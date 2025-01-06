using DataMigration.Domain.Contracts;

namespace DataMigration.Infrastructure.Authentication.Services;

public class MfaService : IMfaService
{
    public string GenerateSecret()
    {
        // TODO: Implement MFA secret generation using a proper library
        throw new NotImplementedException();
    }

    public bool ValidateCode(string secret, string code)
    {
        // TODO: Implement MFA code validation using a proper library
        throw new NotImplementedException();
    }

    public string GenerateQrCodeUri(string secret, string email)
    {
        // TODO: Implement QR code URI generation using a proper library
        throw new NotImplementedException();
    }
} 