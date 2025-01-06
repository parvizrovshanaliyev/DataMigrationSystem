using ErrorOr;
using MediatR;
using DataMigration.Auth.Models;
using DataMigration.Auth.Services;

namespace DataMigration.Auth.Commands.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, ErrorOr<AuthResponse>>
{
    private readonly IGoogleAuthService _googleAuthService;
    private readonly IAuthenticationService _authService;
    private readonly IJwtService _jwtService;

    public GoogleLoginCommandHandler(
        IGoogleAuthService googleAuthService,
        IAuthenticationService authService,
        IJwtService jwtService)
    {
        _googleAuthService = googleAuthService;
        _authService = authService;
        _jwtService = jwtService;
    }

    public async Task<ErrorOr<AuthResponse>> Handle(
        GoogleLoginCommand command,
        CancellationToken cancellationToken)
    {
        var tokenResult = await _googleAuthService.ExchangeCodeAsync(
            command.Code,
            command.RedirectUri,
            command.CodeVerifier);

        if (tokenResult.IsError)
            return tokenResult.Errors;

        var userInfoResult = await _googleAuthService.GetUserInfoAsync(tokenResult.Value.AccessToken);
        
        if (userInfoResult.IsError)
            return userInfoResult.Errors;

        var userResult = await _authService.GetOrCreateGoogleUserAsync(userInfoResult.Value);

        if (userResult.IsError)
            return userResult.Errors;

        var user = userResult.Value;
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
                Picture = user.Picture,
                IsWorkspaceUser = user.IsWorkspaceUser,
                Domain = user.Domain,
                IsMfaEnabled = user.IsMfaEnabled,
                Roles = user.Roles.ToList()
            }
        };
    }
} 