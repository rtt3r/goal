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

    public IConfiguration Configuration { get; private set; }

    public TContext CreateDbContext(string[] args)
        => CreateDbContext(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable(AspNetCoreEnvironment));

    protected abstract TContext CreateNewInstance(DbContextOptionsBuilder<TContext> optionsBuilder);

    private TContext CreateDbContext(string basePath, string environmentName)
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddEnvironmentVariables()
            .Build();

        return CreateNewInstance(new DbContextOptionsBuilder<TContext>());
    }
}
