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

    public IConfiguration Configuration { get; private set; } = null!;

    public TContext CreateDbContext(string[] args)
        => CreateDbContext(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable(AspNetCoreEnvironment));

    protected abstract TContext CreateNewInstance(DbContextOptionsBuilder<TContext> optionsBuilder);

    private TContext CreateDbContext(string basePath, string? environmentName)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json");

        if (environmentName is not null)
        {
            builder = builder.AddJsonFile($"appsettings.{environmentName}.json", true);
        }

        builder = builder.AddEnvironmentVariables();

        Configuration = builder.Build();

        return CreateNewInstance(new DbContextOptionsBuilder<TContext>());
    }
}
