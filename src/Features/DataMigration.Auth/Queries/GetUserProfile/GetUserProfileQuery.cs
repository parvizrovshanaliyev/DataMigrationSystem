using ErrorOr;
using MediatR;
using DataMigration.Auth.Models;

namespace DataMigration.Auth.Queries.GetUserProfile;

public record GetUserProfileQuery : IRequest<ErrorOr<UserResponse>>
{
    public Guid UserId { get; init; }
} 