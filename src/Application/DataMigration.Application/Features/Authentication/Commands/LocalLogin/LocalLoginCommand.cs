using DataMigration.Application.Common.Models;
using DataMigration.Domain.Common;
using DataMigration.Domain.Repositories;
using DataMigration.Domain.Services;
using DataMigration.Domain.ValueObjects;
using MediatR;

namespace DataMigration.Application.Features.Authentication.Commands.LocalLogin;

public record LocalLoginCommand : IRequest<ErrorOr<AuthenticationResult>>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public bool RememberMe { get; init; }
}

public class LocalLoginCommandHandler : IRequestHandler<LocalLoginCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LocalLoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        LocalLoginCommand request,
        CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        if (email is null)
        {
            return Error.Validation("Invalid email format");
        }

        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
        {
            return Error.NotFound("User not found");
        }

        if (user.GoogleId is not null)
        {
            return Error.Validation("Please use Google login for this account");
        }

        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
        {
            return Error.Unauthorized("Account is locked");
        }

        if (user.PasswordHash is null)
        {
            return Error.Validation("Password not set");
        }

        var isValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
        user.RecordLoginAttempt(isValid);
        await _userRepository.UpdateAsync(user);

        if (!isValid)
        {
            return Error.Unauthorized("Invalid credentials");
        }

        if (user.IsMfaEnabled)
        {
            return new AuthenticationResult
            {
                RequiresMfa = true,
                UserId = user.Id
            };
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = request.RememberMe ? _jwtTokenService.GenerateRefreshToken() : null;

        return new AuthenticationResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Picture = user.Picture,
                Roles = user.Roles.Select(r => r.Name).ToList(),
                IsMfaEnabled = user.IsMfaEnabled,
                IsEmailVerified = user.IsEmailVerified,
                IsWorkspaceUser = user.IsWorkspaceUser,
                Domain = user.Domain
            }
        };
    }
} 