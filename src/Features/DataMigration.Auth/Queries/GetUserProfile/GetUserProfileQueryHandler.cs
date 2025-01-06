using ErrorOr;
using MediatR;
using DataMigration.Auth.Models;
using DataMigration.Auth.Services;

namespace DataMigration.Auth.Queries.GetUserProfile;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ErrorOr<UserResponse>>
{
    private readonly IAuthenticationService _authService;

    public GetUserProfileQueryHandler(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task<ErrorOr<UserResponse>> Handle(
        GetUserProfileQuery query,
        CancellationToken cancellationToken)
    {
        var userResult = await _authService.GetUserByIdAsync(query.UserId);

        if (userResult.IsError)
            return userResult.Errors;

        var user = userResult.Value;

        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Picture = user.Picture,
            IsWorkspaceUser = user.IsWorkspaceUser,
            Domain = user.Domain,
            IsMfaEnabled = user.IsMfaEnabled,
            Roles = user.Roles.ToList()
        };
    }
} 