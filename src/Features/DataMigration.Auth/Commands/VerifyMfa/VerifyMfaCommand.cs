using ErrorOr;
using MediatR;
using DataMigration.Auth.Models;

namespace DataMigration.Auth.Commands.VerifyMfa;

public record VerifyMfaCommand : IRequest<ErrorOr<AuthResponse>>
{
    public string MfaToken { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
} 