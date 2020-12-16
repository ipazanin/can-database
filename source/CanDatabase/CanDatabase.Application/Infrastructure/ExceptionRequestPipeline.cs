using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanDatabase.Application.Infrastructure
{
    public class ExceptionRequestPipeline<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {

        #region Fields
        private readonly ILogger<ExceptionRequestPipeline<TRequest, TResponse>> _logger;
        #endregion Fields

        #region Constructors
        public ExceptionRequestPipeline(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionRequestPipeline<TRequest, TResponse>>();
        }
        #endregion Constructors

        #region Methods
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception exception)
            {
                var requestName = typeof(TRequest).Name;
                var requestParameters = request.ToString();

                _logger.LogWarning(
                    exception: exception,
                    message: "Request {0} with parameters {1} threw exception",
                    args: new[] { requestName, requestParameters }
                );

                throw;
            }

        }
    }
    #endregion Methods
}
