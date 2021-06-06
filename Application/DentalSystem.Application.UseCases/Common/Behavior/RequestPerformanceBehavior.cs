using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DentalSystem.Application.UseCases.Common.Behavior
{
    public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public readonly TimeSpan MaxAllowedProcessTime = TimeSpan.FromMilliseconds(500d);

        private readonly Stopwatch _timer;

        public RequestPerformanceBehavior(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            Logger = logger;
        }

        public ILogger<TRequest> Logger { get; }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedTime = _timer.Elapsed;
            if (elapsedTime >= MaxAllowedProcessTime)
            {
                var requestType = typeof(TRequest);
                var requestName = requestType.Name;
                var data = requestType.Namespace.Contains("Identity") ? new { } : request as object;
                Logger.LogWarning(
                    "Long running request detected (elapsed time: {ElapsedTime}): Request: {Name}; Data: {@Request}",
                    elapsedTime,
                    requestName,
                    data
                );
            }

            return response;
        }
    }
}