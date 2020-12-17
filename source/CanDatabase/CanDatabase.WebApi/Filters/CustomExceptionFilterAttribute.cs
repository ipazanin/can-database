using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CanDatabase.Application.Exceptions;
using CanDatabase.Common.Constants;
using CanDatabase.Common.Enums;
using CanDatabase.WebApi.Configuration;
using CanDatabase.WebApi.DataTransferObjects;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CanDatabase.WebApi.Filters
{
    /// <summary>
    /// Custom Exception Filter
    /// Filters thrown exceptions and sets appropriate HTTP Status Code
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        #region Fields
        private readonly IWebApiConfiguration _webApiConfiguration;
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webApiConfiguration"></param>
        /// <param name="slackService"></param>
        /// <param name="loggerService"></param>
        public CustomExceptionFilterAttribute(
            IWebApiConfiguration webApiConfiguration,
            ILoggerFactory loggerFactory
        )
        {
            _webApiConfiguration = webApiConfiguration;
            _logger = loggerFactory.CreateLogger<CustomExceptionFilterAttribute>();
        }
        #endregion 

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError;
            IEnumerable<string>? errors = null;

            if (context.Exception is ValidationException validationException)
            {
                code = HttpStatusCode.BadRequest;
                errors = validationException.Errors.Select(validationFailure => validationFailure.ErrorMessage);
            }
            if (context.Exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            context.HttpContext.Response.ContentType = MimeTypeConstants.Json;
            context.HttpContext.Response.StatusCode = (int)code;

            var exceptionBaseModel = new ExceptionDto(
                exceptionMessage: context.Exception.Message,
                exceptionStackTrace: context.Exception.StackTrace,
                innerExceptionMessage: context.Exception.InnerException?.Message,
                innerExceptionStackTrace: context.Exception.InnerException?.StackTrace,
                errors: errors
            );

            var exceptionModel = _webApiConfiguration.AppConfiguration.AppEnvironment switch
            {
                AppEnvironment.Production => new ExceptionDto(
                    exceptionMessage: MessageConstants.UnhandledExceptionMessage,
                    innerExceptionMessage: null,
                    exceptionStackTrace: null,
                    innerExceptionStackTrace: null,
                    errors: errors
                ),
                _ => exceptionBaseModel,
            };

            context.Result = new JsonResult(
                value: exceptionModel
            );

            _logger.LogWarning(
                exception: context.Exception,
                message: "Unhandled exception with message {0} was caught.",
                args: new object[]
                {
                    context.Exception.Message
                }
            );

            return Task.CompletedTask;
        }
        #endregion
    }
}
