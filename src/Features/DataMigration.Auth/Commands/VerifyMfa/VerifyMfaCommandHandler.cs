using ErrorOr;
using MediatR;
using DataMigration.Auth.Models;
using DataMigration.Auth.Services;

namespace DataMigration.Auth.Commands.VerifyMfa;

public class VerifyMfaCommandHandler : IRequestHandler<VerifyMfaCommand, ErrorOr<AuthResponse>>
{
    private readonly IAuthenticationService _authService;
    private readonly IJwtService _jwtService;
    private readonly ITotpService _totpService;

    public VerifyMfaCommandHandler(
        IAuthenticationService authService,
        IJwtService jwtService,
        ITotpService totpService)
    {
        _authService = authService;
        _jwtService = jwtService;
        _totpService = totpService;
    }

    public async Task<ErrorOr<AuthResponse>> Handle(
        VerifyMfaCommand command,
        CancellationToken cancellationToken)
    {
        var mfaTokenResult = await _jwtService.ValidateMfaTokenAsync(command.MfaToken);
        
        if (mfaTokenResult.IsError)
            return mfaTokenResult.Errors;

        var userResult = await _authService.GetUserByIdAsync(mfaTokenResult.Value.UserId);
        
        if (userResult.IsError)
            return userResult.Errors;

        var user = userResult.Value;

        var isValidCode = await _totpService.ValidateCodeAsync(user.MfaSecret, command.Code);
        if (!isValidCode)
            return Error.Validation("Invalid MFA code");

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
                IsMfaEnabled = true,
                Roles = user.Roles.ToList()
            }
        };
    }
} 