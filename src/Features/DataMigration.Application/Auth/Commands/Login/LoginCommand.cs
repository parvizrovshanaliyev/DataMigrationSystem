using ErrorOr;
using MediatR;
using DataMigration.Application.Auth.Models;

namespace DataMigration.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<ErrorOr<AuthResult>>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool RememberMe { get; init; }
} 