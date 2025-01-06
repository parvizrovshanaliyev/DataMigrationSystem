using DataMigration.Application.Common.Models;
using DataMigration.Domain.Common;
using DataMigration.Domain.Repositories;
using DataMigration.Domain.Services;
using MediatR;
using OtpNet;

namespace DataMigration.Application.Features.Authentication.Commands.VerifyMfa;

public record VerifyMfaCommand : IRequest<ErrorOr<AuthenticationResult>>
{
    public Guid UserId { get; init; }
    public string Code { get; init; } = null!;
    public bool RememberMe { get; init; }
}

public class VerifyMfaCommandHandler : IRequestHandler<VerifyMfaCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public VerifyMfaCommandHandler(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        VerifyMfaCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            return Error.NotFound("User not found");
        }

        if (!user.IsMfaEnabled || user.MfaSecret is null)
        {
            return Error.Validation("MFA is not enabled for this account");
        }

        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
        {
            return Error.Unauthorized("Account is locked");
        }

        var totp = new Totp(Base32Encoding.ToBytes(user.MfaSecret));
        var isValid = totp.VerifyTotp(request.Code, out _, VerificationWindow.RfcSpecifiedNetworkDelay);

        user.RecordLoginAttempt(isValid);
        await _userRepository.UpdateAsync(user);

        if (!isValid)
        {
            return Error.Unauthorized("Invalid MFA code");
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