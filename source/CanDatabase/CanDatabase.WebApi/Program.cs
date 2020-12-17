using System;
using CanDatabase.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CanDatabase.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configureOptions =>
                {
                    var environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableNamesConstants.AspNetCoreEnvironment) ?? "";

                    var configuration = configureOptions
                        .AddJsonFile(path: $"appsettings.json", optional: false)
                        .AddJsonFile(path: $"appsettings.{environmentName}.json", optional: false)
                        .AddEnvironmentVariables()
                        .Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
