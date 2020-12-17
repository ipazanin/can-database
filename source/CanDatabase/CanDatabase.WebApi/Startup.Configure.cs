using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CanDatabase.WebApi.Extensions;

namespace CanDatabase.WebApi
{
    internal sealed partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseSecurityHeadersMiddleware(securityHeadersBuilder =>
            {
                securityHeadersBuilder.AddDefaultSecurePolicy();
            });

            app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var apiVersion in _webApiConfiguration.AppConfiguration.ApiVersions)
                {
                    options.SwaggerEndpoint(
                        url: $"/{SwaggerDocumentDefinitionRoutePrefix}/{apiVersion}/{SwaggerDefinitionFileName}",
                        name: $"{ApiDescriptionNamePrefix} {apiVersion}"
                    );
                }
                options.RoutePrefix = SwaggerApiExplorerRoute;
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseResponseCaching();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
