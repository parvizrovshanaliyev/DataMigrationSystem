using DataMigration.Application.Common.Models;

namespace DataMigration.Api.Models.Responses;

public class AuthResponse
{
    public string AccessToken { get; init; } = null!;
    public string? RefreshToken { get; init; }
    public bool RequiresMfa { get; init; }
    public Guid? UserId { get; init; }
    public UserDto? User { get; init; }

    public AuthResponse() { }

    public AuthResponse(AuthenticationResult result)
    {
        AccessToken = result.AccessToken;
        RefreshToken = result.RefreshToken;
        RequiresMfa = result.RequiresMfa;
        UserId = result.UserId;
        User = result.User;
    }
} 