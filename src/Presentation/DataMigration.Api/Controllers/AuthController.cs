using DataMigration.Api.Models.Requests;
using DataMigration.Api.Models.Responses;
using DataMigration.Application.Features.Authentication.Commands.GoogleLogin;
using DataMigration.Application.Features.Authentication.Commands.LocalLogin;
using DataMigration.Application.Features.Authentication.Commands.RefreshToken;
using DataMigration.Application.Features.Authentication.Commands.VerifyMfa;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataMigration.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login/local")]
    public async Task<ActionResult<AuthResponse>> LocalLogin(LocalLoginRequest request)
    {
        var command = new LocalLoginCommand
        {
            Email = request.Email,
            Password = request.Password,
            RememberMe = request.RememberMe
        };

        var result = await _mediator.Send(command);

        if (result.IsError)
        {
            return result.FirstError.Type switch
            {
                ErrorType.NotFound => NotFound(result.FirstError.Description),
                ErrorType.Validation => BadRequest(result.FirstError.Description),
                ErrorType.Unauthorized => Unauthorized(result.FirstError.Description),
                _ => StatusCode(500, result.FirstError.Description)
            };
        }

        return Ok(new AuthResponse(result.Value));
    }

    [HttpPost("login/google")]
    public async Task<ActionResult<AuthResponse>> GoogleLogin(GoogleLoginRequest request)
    {
        var command = new GoogleLoginCommand
        {
            IdToken = request.IdToken,
            RememberMe = request.RememberMe
        };

        var result = await _mediator.Send(command);

        if (result.IsError)
        {
            return result.FirstError.Type switch
            {
                ErrorType.NotFound => NotFound(result.FirstError.Description),
                ErrorType.Validation => BadRequest(result.FirstError.Description),
                ErrorType.Unauthorized => Unauthorized(result.FirstError.Description),
                _ => StatusCode(500, result.FirstError.Description)
            };
        }

        return Ok(new AuthResponse(result.Value));
    }

    [HttpPost("mfa/verify")]
    public async Task<ActionResult<AuthResponse>> VerifyMfa(VerifyMfaRequest request)
    {
        var command = new VerifyMfaCommand
        {
            UserId = request.UserId,
            Code = request.Code,
            RememberMe = request.RememberMe
        };

        var result = await _mediator.Send(command);

        if (result.IsError)
        {
            return result.FirstError.Type switch
            {
                ErrorType.NotFound => NotFound(result.FirstError.Description),
                ErrorType.Validation => BadRequest(result.FirstError.Description),
                ErrorType.Unauthorized => Unauthorized(result.FirstError.Description),
                _ => StatusCode(500, result.FirstError.Description)
            };
        }

        return Ok(new AuthResponse(result.Value));
    }

    [HttpPost("token/refresh")]
    public async Task<ActionResult<AuthResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var command = new RefreshTokenCommand
        {
            AccessToken = request.AccessToken,
            RefreshToken = request.RefreshToken
        };

        var result = await _mediator.Send(command);

        if (result.IsError)
        {
            return result.FirstError.Type switch
            {
                ErrorType.NotFound => NotFound(result.FirstError.Description),
                ErrorType.Validation => BadRequest(result.FirstError.Description),
                ErrorType.Unauthorized => Unauthorized(result.FirstError.Description),
                _ => StatusCode(500, result.FirstError.Description)
            };
        }

        return Ok(new AuthResponse(result.Value));
    }

    [Authorize]
    [HttpPost("logout")]
    public ActionResult Logout()
    {
        // In a real application, you would invalidate the refresh token here
        return Ok();
    }

    [Authorize]
    [HttpGet("profile")]
    public ActionResult<UserResponse> GetProfile()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        // In a real application, you would fetch the user profile from the database
        return Ok(new UserResponse
        {
            Id = Guid.Parse(userId),
            Email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value,
            Name = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value,
            Picture = User.FindFirst("picture")?.Value,
            Roles = User.FindAll(System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList()
        });
    }
} 