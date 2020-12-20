using System.Collections.Generic;
using CanDatabase.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CanDatabase.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public AppEnvironment AppEnvironment { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Version => _configuration.GetValue<string>("Version");

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion ApiVersion => new(
            majorVersion: _configuration.GetValue<int>("MajorVersion"),
            minorVersion: _configuration.GetValue<int>("MinorVersion")
        );

        /// <summary>
        /// All Api Versions
        /// </summary>
        /// <value></value>
        public IEnumerable<ApiVersion> ApiVersions => new[]
        {
            ApiVersion,
        };
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public AppConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}
