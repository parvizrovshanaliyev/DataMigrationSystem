using ErrorOr;
using DataMigration.Domain.Entities;
using DataMigration.Domain.Events;
using DataMigration.Domain.Repositories;
using DataMigration.Domain.Services;
using DataMigration.Auth.Models;
using DataMigration.Common.Services;

namespace DataMigration.Auth.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;
    private readonly IEventBus _eventBus;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IEmailService emailService,
        ITokenService tokenService,
        IEventBus eventBus)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
        _tokenService = tokenService;
        _eventBus = eventBus;
    }

    public async Task<ErrorOr<User>> AuthenticateLocalAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
            return Error.NotFound("User not found");

        if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
            return Error.Failure("Account is locked out");

        if (!await _passwordHasher.ValidateAsync(password, user.PasswordHash!))
        {
            user.RecordLoginAttempt(false);
            await _userRepository.UpdateAsync(user);
            return Error.Validation("Invalid credentials");
        }

        user.RecordLoginAttempt(true);
        await _userRepository.UpdateAsync(user);
        return user;
    }

    public async Task<ErrorOr<User>> GetOrCreateGoogleUserAsync(GoogleUserInfo userInfo)
    {
        var user = await _userRepository.GetByEmailAsync(userInfo.Email);
        if (user is not null)
        {
            if (user.GoogleId != userInfo.Id)
            {
                user.UpdateProfile(userInfo.Name, userInfo.Picture);
                await _userRepository.UpdateAsync(user);
            }
            return user;
        }

        user = User.CreateFromGoogle(
            userInfo.Email,
            userInfo.Id,
            userInfo.Name,
            userInfo.Picture,
            userInfo.Domain);

        await _userRepository.AddAsync(user);
        return user;
    }

    public async Task<ErrorOr<User>> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            return Error.NotFound("User not found");

        return user;
    }

    public async Task<ErrorOr<User>> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
            return Error.NotFound("User not found");

        return user;
    }

    public async Task<ErrorOr<Success>> UpdateUserProfileAsync(Guid userId, string name, string? picture)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            return Error.NotFound("User not found");

        user.UpdateProfile(name, picture);
        await _userRepository.UpdateAsync(user);
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> EnableMfaAsync(Guid userId, string secret)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            return Error.NotFound("User not found");

        user.EnableMfa(secret);
        await _userRepository.UpdateAsync(user);
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> DisableMfaAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            return Error.NotFound("User not found");

        user.DisableMfa();
        await _userRepository.UpdateAsync(user);
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            return Error.NotFound("User not found");

        if (!await _passwordHasher.ValidateAsync(currentPassword, user.PasswordHash!))
            return Error.Validation("Current password is incorrect");

        var newHash = await _passwordHasher.HashAsync(newPassword);
        user.Apply(new PasswordSetEvent(newHash));
        await _userRepository.UpdateAsync(user);
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
            return Error.NotFound("User not found");

        if (!await _tokenService.ValidateTokenAsync(token))
            return Error.Validation("Invalid or expired token");

        var newHash = await _passwordHasher.HashAsync(newPassword);
        user.Apply(new PasswordSetEvent(newHash));
        await _userRepository.UpdateAsync(user);
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> RequestPasswordResetAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
            return Error.NotFound("User not found");

        var token = await _tokenService.GenerateTokenAsync();
        await _emailService.SendPasswordResetEmailAsync(email, token);
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> VerifyEmailAsync(string email, string token)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
            return Error.NotFound("User not found");

        if (!await _tokenService.ValidateTokenAsync(token))
            return Error.Validation("Invalid or expired token");

        user.VerifyEmail();
        await _userRepository.UpdateAsync(user);
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> RequestEmailVerificationAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
            return Error.NotFound("User not found");

        var token = await _tokenService.GenerateTokenAsync();
        await _emailService.SendEmailVerificationAsync(email, token);
        return Result.Success;
    }
} 