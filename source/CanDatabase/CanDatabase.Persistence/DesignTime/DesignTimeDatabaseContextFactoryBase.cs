using System;
using System.IO;
using CanDatabase.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CanDatabase.Infrastructure.Persistence.DesignTime
{
    public abstract class DesignTimeDatabaseContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        #region Properties
        private static string ConfigurationBasePath => $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}CanDatabase.WebApi";
        private static string ConfigurationFileName => $"appsettings.{Environment.GetEnvironmentVariable(EnvironmentVariableNamesConstants.AspNetCoreEnvironment)}.json";
        private static string ConnectionStringConfigurationPath => "DatabaseConfiguration:DefaultConnectionString";
        #endregion

        #region Constructors
        protected DesignTimeDatabaseContextFactoryBase()
        {
        }
        #endregion

        #region Methods
        public TContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(ConfigurationBasePath)
                .AddJsonFile(ConfigurationFileName, optional: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration[ConnectionStringConfigurationPath];

            return Create(connectionString);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        /// <summary>
        /// TODO: Add multiple providers
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"{nameof(DesignTimeDatabaseContextFactoryBase<TContext>)}.{nameof(Create)}(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
        #endregion
    }
}
