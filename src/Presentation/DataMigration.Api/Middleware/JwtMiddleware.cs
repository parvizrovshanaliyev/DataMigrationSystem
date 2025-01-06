using System.Security.Claims;
using DataMigration.Domain.Repositories;
using DataMigration.Domain.Services;
using Microsoft.AspNetCore.Http;

namespace DataMigration.Api.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJwtTokenService _jwtTokenService;

    public JwtMiddleware(RequestDelegate next, IJwtTokenService jwtTokenService)
    {
        _next = next;
        _jwtTokenService = jwtTokenService;
    }

    public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            try
            {
                var principal = _jwtTokenService.ValidateToken(token);
                if (principal != null)
                {
                    var userId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                    var user = await userRepository.GetByIdAsync(userId);

                    if (user != null && (!user.LockoutEnd.HasValue || user.LockoutEnd.Value <= DateTime.UtcNow))
                    {
                        context.User = principal;
                    }
                }
            }
            catch
            {
                // Token validation failed
            }
        }

        await _next(context);
    }
} 