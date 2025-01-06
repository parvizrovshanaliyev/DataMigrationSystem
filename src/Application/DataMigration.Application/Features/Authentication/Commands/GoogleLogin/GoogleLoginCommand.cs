using DataMigration.Application.Common.Models;
using DataMigration.Domain.Common;
using DataMigration.Domain.Repositories;
using DataMigration.Domain.Services;
using DataMigration.Domain.ValueObjects;
using Google.Apis.Auth;
using MediatR;

namespace DataMigration.Application.Features.Authentication.Commands.GoogleLogin;

public record GoogleLoginCommand : IRequest<ErrorOr<AuthenticationResult>>
{
    public string IdToken { get; init; } = null!;
    public bool RememberMe { get; init; }
}

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly GoogleAuthSettings _googleAuthSettings;

    public GoogleLoginCommandHandler(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService,
        GoogleAuthSettings googleAuthSettings)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _googleAuthSettings = googleAuthSettings;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        GoogleLoginCommand request,
        CancellationToken cancellationToken)
    {
        GoogleJsonWebSignature.Payload? payload;
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _googleAuthSettings.ClientId }
            };
            payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
        }
        catch (InvalidJwtException)
        {
            return Error.Unauthorized("Invalid Google token");
        }

        var email = Email.Create(payload.Email);
        if (email is null)
        {
            return Error.Validation("Invalid email format");
        }

        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
        {
            user = Domain.Entities.User.CreateFromGoogle(
                email,
                payload.Subject,
                payload.Name,
                payload.Picture,
                payload.HostedDomain
            );
            await _userRepository.AddAsync(user);
        }
        else if (user.GoogleId != payload.Subject)
        {
            return Error.Validation("Email already registered with different method");
        }

        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
        {
            return Error.Unauthorized("Account is locked");
        }

        user.RecordLoginAttempt(true);
        await _userRepository.UpdateAsync(user);

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

public class GoogleAuthSettings
{
    public string ClientId { get; init; } = null!;
} 