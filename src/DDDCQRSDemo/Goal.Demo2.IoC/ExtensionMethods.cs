using Goal.Application.Services;
using Goal.Demo22.Application.People;
using Goal.Demo22.Infra.Data;
using Goal.Demo22.Infra.Data.Repositories;
using Goal.Domain;
using Goal.Domain.Aggregates;
using Goal.Infra.Data;
using Goal.Infra.Http.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goal.Demo22.IoC
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddHttpContextAccessor();
            //services.AddScoped<ElasticAuditChangesInterceptor>();

            services
                .AddDbContext<Demo22Context>((provider, options) =>
                {
                    options
                        .UseSqlite(
                            connectionString,
                            opts => opts.MigrationsAssembly(typeof(Demo22Context).Assembly.GetName().Name))
                        .EnableSensitiveDataLogging();

                    //options.AddInterceptors(provider.GetRequiredService<ElasticAuditChangesInterceptor>());
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.RegisterAllTypesOf<IRepository>(typeof(PersonRepository).Assembly);
            services.RegisterAllTypesOf<IAppService>(typeof(PersonAppService).Assembly);

            return services;
        }
    }
}
