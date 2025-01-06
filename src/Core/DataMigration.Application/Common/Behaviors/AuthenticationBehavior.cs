//using System.Security.Claims;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using DataMigration.Application.Common.Interfaces;
//using DataMigration.Application.Common.Attributes;

//namespace DataMigration.Application.Common.Behaviors;

//public class AuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//{
//    private readonly IHttpContextAccessor _httpContextAccessor;
//    private readonly ITokenManager _tokenManager;

//    public AuthenticationBehavior(
//        IHttpContextAccessor httpContextAccessor,
//        ITokenManager tokenManager)
//    {
//        _httpContextAccessor = httpContextAccessor;
//        _tokenManager = tokenManager;
//    }

//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next,
//        CancellationToken cancellationToken)
//    {
//        var requiresAuth = !typeof(TRequest).GetCustomAttributes(true)
//            .Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

//        if (!requiresAuth)
//        {
//            return await next();
//        }

//        var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
//        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
//        {
//            throw new UnauthorizedAccessException("No valid authentication token provided");
//        }

//        var token = authHeader.Substring("Bearer ".Length);
//        var tokenInfo = await _tokenManager.ValidateTokenAsync(token);

//        if (tokenInfo == null)
//        {
//            throw new UnauthorizedAccessException("Invalid or expired token");
//        }

//        // Set the user context
//        var claims = new List<Claim>
//        {
//            new Claim(ClaimTypes.NameIdentifier, tokenInfo.UserId),
//            new Claim(ClaimTypes.Name, tokenInfo.UserId)
//        };

//        foreach (var role in tokenInfo.Roles)
//        {
//            claims.Add(new Claim(ClaimTypes.Role, role));
//        }

//        var identity = new ClaimsIdentity(claims, "Bearer");
//        _httpContextAccessor.HttpContext!.User = new ClaimsPrincipal(identity);

//        return await next();
//    }
//}