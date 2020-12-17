using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanDatabase.Application.Infrastructure
{
    public class LoggerRequestPipeline<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        #region Fields
        private readonly Stopwatch _stopWatch;
        private readonly ILogger<LoggerRequestPipeline<TRequest, TResponse>> _logger;
        #endregion

        #region Constructors
        public LoggerRequestPipeline(
            ILoggerFactory loggerFactory
        )
        {
            _stopWatch = new Stopwatch();
            _logger = loggerFactory.CreateLogger<LoggerRequestPipeline<TRequest, TResponse>>();
        }
        #endregion

        #region Methods
        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next
        )
        {
            _stopWatch.Start();
            var response = await next();
            _stopWatch.Stop();

            var requestName = typeof(TRequest).Name;
            var requestParameters = request.ToString() ?? string.Empty;
            var duration = _stopWatch.Elapsed;
            var responseParameters = response?.ToString() ?? string.Empty;

            _logger.LogInformation(
                message: "Request {0} with parameters {1} completed after {2} with response {3}",
                args: new object[] { requestName, requestParameters, duration, responseParameters }
            );

            _stopWatch.Reset();

            return response;
        }
        #endregion
    }
}
