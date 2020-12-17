using System.Collections.Generic;

namespace CanDatabase.WebApi.Middleware.SecurityHeaders
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityHeadersPolicy
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> SetHeaders { get; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
    }
}
