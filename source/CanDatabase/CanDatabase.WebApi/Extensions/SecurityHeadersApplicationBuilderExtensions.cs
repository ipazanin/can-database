using System;
using CanDatabase.WebApi.Middleware.SecurityHeaders;
using Microsoft.AspNetCore.Builder;

namespace CanDatabase.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SecurityHeadersApplicationBuilderExtensions
    {
        /// <summary>
        /// Sets Security Headers
        /// </summary>
        /// <param name="app"></param>
        /// <param name="securityHeadersBuilderConfigurationAction"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSecurityHeadersMiddleware(
            this IApplicationBuilder app,
            Action<SecurityHeadersBuilder> securityHeadersBuilderConfigurationAction
        )
        {
            var builder = new SecurityHeadersBuilder();

            securityHeadersBuilderConfigurationAction.Invoke(builder);

            var policy = builder.Build();

            return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
        }
    }
}
