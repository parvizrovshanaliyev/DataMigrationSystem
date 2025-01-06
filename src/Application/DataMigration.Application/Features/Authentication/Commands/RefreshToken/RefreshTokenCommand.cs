using DataMigration.Application.Common.Models;
using DataMigration.Domain.Common;
using DataMigration.Domain.Repositories;
using DataMigration.Domain.Services;
using MediatR;

namespace DataMigration.Application.Features.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<ErrorOr<AuthenticationResult>>
{
    public string AccessToken { get; init; } = null!;
    public string RefreshToken { get; init; } = null!;
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public RefreshTokenCommandHandler(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var principal = _jwtTokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal is null)
        {
            return Error.Unauthorized("Invalid access token");
        }

        var userId = Guid.Parse(principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user is null)
        {
            return Error.NotFound("User not found");
        }

        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
        {
            return Error.Unauthorized("Account is locked");
        }

        // In a real application, you would validate the refresh token against a stored value
        // and ensure it hasn't been revoked or expired

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

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