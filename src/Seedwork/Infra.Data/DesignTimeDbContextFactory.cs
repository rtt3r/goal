using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Goal.Seedwork.Infra.Data;

public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
{
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    private const string DefaultConnectionStringName = "DefaultConnection";

    protected virtual string ConnectionStringName
        => DefaultConnectionStringName;

    public TContext CreateDbContext(string[] args)
        => CreateDbContext(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable(AspNetCoreEnvironment));

    protected abstract TContext CreateNewInstance(DbContextOptionsBuilder<TContext> optionsBuilder, string connectionString);

    private TContext CreateDbContext(string basePath, string environmentName)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddEnvironmentVariables()
            .Build();

        string connectionString = config.GetConnectionString(ConnectionStringName);

        return string.IsNullOrWhiteSpace(connectionString)
            ? throw new InvalidOperationException($"Could not find a connection string named '{ConnectionStringName}'.")
            : CreateNewInstance(
            new DbContextOptionsBuilder<TContext>(),
            connectionString);
    }
}
