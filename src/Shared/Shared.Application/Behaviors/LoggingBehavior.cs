using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Abstractions.Time;

namespace Shared.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger,
                                IDateTimeProvider dateTimeProvider)
        {
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task<TResponse> Handle(TRequest request,
                                            RequestHandlerDelegate<TResponse> next,
                                            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting request: {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                _dateTimeProvider.LocalTimeNow);

            var response = await next();

            _logger.LogInformation("Completed request: {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                _dateTimeProvider.LocalTimeNow);

            return response;
        }
    }
}
