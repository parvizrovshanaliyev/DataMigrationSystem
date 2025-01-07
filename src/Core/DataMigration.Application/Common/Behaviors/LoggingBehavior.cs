using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using DataMigration.Application.Common.Interfaces;

namespace DataMigration.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior for logging command/query execution
/// </summary>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly ICurrentUser _currentUser;
    private readonly IDateTime _dateTime;

    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger,
        ICurrentUser currentUser,
        IDateTime dateTime)
    {
        _logger = logger;
        _currentUser = currentUser;
        _dateTime = dateTime;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUser.Id;
        var userName = _currentUser.UserName;

        _logger.LogInformation(
            "Handling {RequestName} for user {UserId} ({UserName}) at {DateTime}",
            requestName,
            userId,
            userName,
            _dateTime.Now);

        try
        {
            var response = await next();

            _logger.LogInformation(
                "Completed {RequestName} for user {UserId} ({UserName}) at {DateTime}",
                requestName,
                userId,
                userName,
                _dateTime.Now);

            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Error handling {RequestName} for user {UserId} ({UserName}) at {DateTime}",
                requestName,
                userId,
                userName,
                _dateTime.Now);

            throw;
        }
    }
} 