using ErrorOr;
using MediatR;
using DataMigration.Auth.Models;

namespace DataMigration.Auth.Commands.GoogleLogin;

public record GoogleLoginCommand : IRequest<ErrorOr<AuthResponse>>
{
    public string Code { get; init; } = string.Empty;
    public string RedirectUri { get; init; } = string.Empty;
    public string CodeVerifier { get; init; } = string.Empty;
} 