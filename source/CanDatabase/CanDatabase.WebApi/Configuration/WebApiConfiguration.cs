using Microsoft.Extensions.Configuration;

namespace CanDatabase.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiConfiguration : IWebApiConfiguration
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public AppConfiguration AppConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DatabaseConfiguration DatabaseConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public SystemTextJsonSerializerConfiguration SystemTextJsonSerializerConfiguration { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public WebApiConfiguration(IConfiguration configuration)
        {
            AppConfiguration = new AppConfiguration(configuration.GetSection("AppConfiguration"));
            DatabaseConfiguration = new DatabaseConfiguration(configuration.GetSection("DatabaseConfiguration"));
            SystemTextJsonSerializerConfiguration = new SystemTextJsonSerializerConfiguration(configuration.GetSection("SystemTextJsonSerializerConfiguration"));
        }
        #endregion
    }
}
