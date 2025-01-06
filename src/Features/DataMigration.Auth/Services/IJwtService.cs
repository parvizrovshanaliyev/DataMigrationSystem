using ErrorOr;
using DataMigration.Domain.Entities;
using DataMigration.Auth.Models;

namespace DataMigration.Auth.Services;

public interface IJwtService
{
    Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(User user);
    Task<string> GenerateMfaTokenAsync(Guid userId);
    Task<ErrorOr<TokenInfo>> ValidateAccessTokenAsync(string token);
    Task<ErrorOr<TokenInfo>> ValidateRefreshTokenAsync(string token);
    Task<ErrorOr<TokenInfo>> ValidateMfaTokenAsync(string token);
    Task RevokeTokenAsync(string token);
} 