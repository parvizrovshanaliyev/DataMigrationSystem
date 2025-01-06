using ErrorOr;
using DataMigration.Domain.Entities;
using DataMigration.Auth.Models;

namespace DataMigration.Auth.Services;

public interface IAuthenticationService
{
    Task<ErrorOr<User>> AuthenticateLocalAsync(string email, string password);
    Task<ErrorOr<User>> GetOrCreateGoogleUserAsync(GoogleUserInfo userInfo);
    Task<ErrorOr<User>> GetUserByIdAsync(Guid userId);
    Task<ErrorOr<User>> GetUserByEmailAsync(string email);
    Task<ErrorOr<Success>> UpdateUserProfileAsync(Guid userId, string name, string? picture);
    Task<ErrorOr<Success>> EnableMfaAsync(Guid userId, string secret);
    Task<ErrorOr<Success>> DisableMfaAsync(Guid userId);
    Task<ErrorOr<Success>> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    Task<ErrorOr<Success>> ResetPasswordAsync(string email, string token, string newPassword);
    Task<ErrorOr<Success>> RequestPasswordResetAsync(string email);
    Task<ErrorOr<Success>> VerifyEmailAsync(string email, string token);
    Task<ErrorOr<Success>> RequestEmailVerificationAsync(string email);
} 