//using System.Reflection;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using DataMigration.Application.Common.Interfaces;
//using DataMigration.Application.Common.Security;

//namespace DataMigration.Application.Common.Behaviors;

//public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//{
//    private readonly IHttpContextAccessor _httpContextAccessor;
//    private readonly IAuthorizationService _authorizationService;

//    public AuthorizationBehavior(
//        IHttpContextAccessor httpContextAccessor,
//        IAuthorizationService authorizationService)
//    {
//        _httpContextAccessor = httpContextAccessor;
//        _authorizationService = authorizationService;
//    }

//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next,
//        CancellationToken cancellationToken)
//    {
//        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

//        if (!authorizeAttributes.Any())
//        {
//            return await next();
//        }

//        var user = _httpContextAccessor.HttpContext?.User;
//        if (user == null || !user.Identity!.IsAuthenticated)
//        {
//            throw new UnauthorizedAccessException("User is not authenticated");
//        }

//        foreach (var attribute in authorizeAttributes)
//        {
//            var authorized = false;

//            if (!string.IsNullOrEmpty(attribute.Roles))
//            {
//                var roles = attribute.Roles.Split(',');
//                authorized = roles.Any(role => user.IsInRole(role.Trim()));
//            }
//            else if (!string.IsNullOrEmpty(attribute.Policy))
//            {
//                var result = await _authorizationService.AuthorizeAsync(user, attribute.Policy);
//                authorized = result.Succeeded;
//            }
//            else
//            {
//                authorized = true; // No specific roles or policies required
//            }

//            if (!authorized)
//            {
//                throw new ForbiddenAccessException("User does not have required permissions");
//            }
//        }

//        return await next();
//    }
//}