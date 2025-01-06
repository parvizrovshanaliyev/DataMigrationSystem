using ErrorOr;
using MediatR;
using DataMigration.Auth.Models;

namespace DataMigration.Auth.Commands.LocalLogin;

public record LocalLoginCommand : IRequest<ErrorOr<AuthResponse>>
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool RememberMe { get; init; }
} 