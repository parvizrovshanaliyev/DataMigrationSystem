using DataMigration.Application.Common.Models;

namespace DataMigration.Application.Common.Interfaces;

public interface ITokenManager
{
    Task<TokenInfo> ValidateTokenAsync(string token);
    Task<TokenInfo> RefreshTokenAsync(string refreshToken);
    Task RevokeTokenAsync(string token);
    Task<bool> IsTokenBlacklistedAsync(string token);
}