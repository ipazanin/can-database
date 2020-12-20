using CanDatabase.Common.Constants;
using CanDatabase.WebApi.Configuration;
using Microsoft.Extensions.Configuration;

namespace CanDatabase.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Startup
    {
        #region Constants
        private const string SwaggerDefinitionFileName = "swagger" + FileExtensionConstants.Json;
        private const string ApiDescriptionNamePrefix = "CanDatabase API";
        private const string SwaggerApiExplorerRoute = "docs";
        private const string SwaggerDocumentDefinitionRoutePrefix = "swagger";
        #endregion

        #region Fields
        private readonly IWebApiConfiguration _webApiConfiguration;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            _webApiConfiguration = new WebApiConfiguration(configuration);
        }
        #endregion
    }
}
