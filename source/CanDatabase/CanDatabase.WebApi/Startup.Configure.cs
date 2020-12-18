using Microsoft.AspNetCore.Builder;
using CanDatabase.WebApi.Extensions;
using CanDatabase.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace CanDatabase.WebApi
{
    internal sealed partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="databaseContext"></param>
        public void Configure(
            IApplicationBuilder app,
            ICanDatabaseContext databaseContext
        )
        {
            if (_webApiConfiguration.AppConfiguration.AppEnvironment == Common.Enums.AppEnvironment.Development)
            {
                app.UseWebAssemblyDebugging();
            }

            databaseContext.Database.Migrate();

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

            app.UseExceptionHandler("/Error");
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

        }
    }
}
