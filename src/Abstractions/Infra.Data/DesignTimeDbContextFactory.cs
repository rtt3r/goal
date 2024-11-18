using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Goal.Infra.Data;

public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
{
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    private const string AppSettingsFileName = "appsettings.json";

    private IConfiguration? _configuration;

    protected IConfiguration Configuration => _configuration ??= BuildConfiguration();

    public TContext CreateDbContext(string[] args)
        => CreateDbContext();

    protected abstract TContext CreateNewInstance(DbContextOptionsBuilder<TContext> optionsBuilder);

    private TContext CreateDbContext()
        => CreateNewInstance(new DbContextOptionsBuilder<TContext>());

    private static IConfiguration BuildConfiguration()
    {
        string? environmentName = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);

        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppSettingsFileName)
            .AddEnvironmentVariables();

        if (!string.IsNullOrEmpty(environmentName))
        {
            builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
        }

        return builder.Build();
    }
}
