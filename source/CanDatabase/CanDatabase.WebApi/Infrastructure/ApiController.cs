using CanDatabase.WebApi.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CanDatabase.WebApi.Infrastructure
{
    /// <summary>
    /// ApiController
    /// </summary>
    [ApiController]
    public class ApiController : ControllerBase
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        protected readonly ISender Sender;

        /// <summary>
        /// 
        /// </summary>
        protected readonly IWebApiConfiguration WebApiConfiguration;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// ApiController Constructor
        /// </summary>
        public ApiController(
            ISender sender,
            IWebApiConfiguration webApiConfiguration
        )
        {
            Sender = sender;
            WebApiConfiguration = webApiConfiguration;
        }
        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
