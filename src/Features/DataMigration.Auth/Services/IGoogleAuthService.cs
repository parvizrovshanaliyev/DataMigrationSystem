using ErrorOr;
using DataMigration.Auth.Models;

namespace DataMigration.Auth.Services;

public interface IGoogleAuthService
{
    Task<ErrorOr<GoogleTokenResponse>> ExchangeCodeAsync(
        string code,
        string redirectUri,
        string codeVerifier);
    Task<ErrorOr<GoogleUserInfo>> GetUserInfoAsync(string accessToken);
    Task<ErrorOr<Success>> ValidateTokenAsync(string token);
    Task<string> GenerateAuthUrlAsync(string redirectUri, string codeChallenge);
} 