namespace CanDatabase.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebApiConfiguration
    {
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
    }
}
