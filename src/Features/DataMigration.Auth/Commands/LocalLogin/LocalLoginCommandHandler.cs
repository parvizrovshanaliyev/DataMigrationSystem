using ErrorOr;
using MediatR;
using DataMigration.Auth.Models;
using DataMigration.Auth.Services;

namespace DataMigration.Auth.Commands.LocalLogin;

public class LocalLoginCommandHandler : IRequestHandler<LocalLoginCommand, ErrorOr<AuthResponse>>
{
    private readonly IAuthenticationService _authService;
    private readonly IJwtService _jwtService;
    private readonly ITotpService _totpService;

    public LocalLoginCommandHandler(
        IAuthenticationService authService,
        IJwtService jwtService,
        ITotpService totpService)
    {
        _authService = authService;
        _jwtService = jwtService;
        _totpService = totpService;
    }

    public async Task<ErrorOr<AuthResponse>> Handle(
        LocalLoginCommand command,
        CancellationToken cancellationToken)
    {
        var authResult = await _authService.AuthenticateLocalAsync(
            command.Username,
            command.Password);

        if (authResult.IsError)
            return authResult.Errors;

        var user = authResult.Value;

        if (user.IsMfaEnabled)
        {
            var mfaToken = await _jwtService.GenerateMfaTokenAsync(user.Id);
            return new AuthResponse
            {
                RequiresMfa = true,
                MfaToken = mfaToken
            };
        }

        var (accessToken, refreshToken) = await _jwtService.GenerateTokensAsync(user);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 3600,
            User = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                IsWorkspaceUser = false,
                IsMfaEnabled = user.IsMfaEnabled,
                Roles = user.Roles.ToList()
            }
        };
    }
} 