using System.Collections.Generic;

namespace CanDatabase.WebApi.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionDto
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ExceptionMessage { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string InnerExceptionMessage { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ExceptionStackTrace { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string InnerExceptionStackTrace { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IEnumerable<string>? Errors { get; }
        #endregion

        #region  Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionMessage"></param>
        /// <param name="innerExceptionMessage"></param>
        /// <param name="exceptionStackTrace"></param>
        /// <param name="innerExceptionStackTrace"></param>
        /// <param name="errors"></param>
        public ExceptionDto(
            string exceptionMessage,
            string? innerExceptionMessage,
            string? exceptionStackTrace,
            string? innerExceptionStackTrace,
            IEnumerable<string>? errors
        )
        {
            ExceptionMessage = exceptionMessage;
            InnerExceptionMessage = innerExceptionMessage ?? string.Empty;
            ExceptionStackTrace = exceptionStackTrace ?? string.Empty;
            InnerExceptionStackTrace = innerExceptionStackTrace ?? string.Empty;
            Errors = errors;
        }
        #endregion
    }
}
