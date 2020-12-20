using System;
using System.Linq;
using CanDatabase.Common.Constants;
using CanDatabase.Persistence.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CanDatabase.WebApiIntegrationTests.Infrastructure
{
    /// <summary>
    /// CustomWebApplicationFactory
    /// </summary>
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class

    {
        #region Constructors
        /// <summary>
        /// CustomWebApplicationFactory Constructor
        /// </summary>
        public CustomWebApplicationFactory()
        {
            Environment.SetEnvironmentVariable(
                variable: EnvironmentVariableNamesConstants.AspNetCoreEnvironment,
                value: "development"
            );
        }
        #endregion Constructors

        #region Methods
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<CanDatabaseContext>)
                    );
                services.Remove(descriptor);

                services.AddDbContext<ICanDatabaseContext, CanDatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForIntegrationTesting");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ICanDatabaseContext>();
                db.Database.EnsureCreated();
            });
        }
        #endregion Methods
    }
}
