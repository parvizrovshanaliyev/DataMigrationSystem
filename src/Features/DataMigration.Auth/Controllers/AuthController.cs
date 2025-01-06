using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DataMigration.Auth.Models;
using DataMigration.Auth.Models.Requests;
using DataMigration.Auth.Commands.LocalLogin;
using DataMigration.Auth.Commands.GoogleLogin;
using DataMigration.Auth.Queries.GetUserProfile;
using DataMigration.Auth.Commands.VerifyMfa;
using DataMigration.Auth.Commands.RefreshToken;
using DataMigration.Auth.Commands.Logout;

namespace DataMigration.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("local/login")]
    public async Task<ActionResult<AuthResponse>> LocalLogin([FromBody] LocalLoginRequest request)
    {
        var command = new LocalLoginCommand
        {
            Username = request.Username,
            Password = request.Password,
            RememberMe = request.RememberMe
        };

        var result = await _mediator.Send(command);

        return result.Match(
            auth => Ok(auth),
            errors => Problem(errors));
    }

    [HttpPost("google/login")]
    public async Task<ActionResult<AuthResponse>> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        var command = new GoogleLoginCommand
        {
            Code = request.Code,
            RedirectUri = request.RedirectUri,
            CodeVerifier = request.CodeVerifier
        };

        var result = await _mediator.Send(command);

        return result.Match(
            auth => Ok(auth),
            errors => Problem(errors));
    }

    [HttpPost("mfa/verify")]
    public async Task<ActionResult<AuthResponse>> VerifyMfa([FromBody] VerifyMfaRequest request)
    {
        var command = new VerifyMfaCommand
        {
            MfaToken = request.MfaToken,
            Code = request.Code
        };

        var result = await _mediator.Send(command);

        return result.Match(
            auth => Ok(auth),
            errors => Problem(errors));
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var command = new RefreshTokenCommand
        {
            RefreshToken = request.RefreshToken
        };

        var result = await _mediator.Send(command);

        return result.Match(
            auth => Ok(auth),
            errors => Problem(errors));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var command = new LogoutCommand
        {
            UserId = User.GetUserId(),
            AccessToken = HttpContext.GetAccessToken()
        };

        var result = await _mediator.Send(command);

        return result.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<UserResponse>> GetProfile()
    {
        var query = new GetUserProfileQuery { UserId = User.GetUserId() };
        var result = await _mediator.Send(query);

        return result.Match(
            profile => Ok(profile),
            errors => Problem(errors));
    }

    private IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
            return Problem();

        if (errors.All(e => e.Type == ErrorType.Validation))
            return ValidationProblem(errors);

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(
            title: firstError.Description,
            statusCode: statusCode);
    }
} 