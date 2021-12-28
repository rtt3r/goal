using Goal.Application.Services;
using Goal.DemoCqrs.Infra.Data;
using Goal.DemoCqrs.Infra.Data.Repositories;
using Goal.Domain;
using Goal.Domain.Aggregates;
using Goal.Infra.Data;
using Goal.Infra.Http.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goal.DemoCqrs.IoC
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddHttpContextAccessor();
            //services.AddScoped<ElasticAuditChangesInterceptor>();

            services
                .AddDbContext<DemoCqrsContext>((provider, options) =>
                {
                    options
                        .UseSqlite(
                            connectionString,
                            opts => opts.MigrationsAssembly(typeof(DemoCqrsContext).Assembly.GetName().Name))
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
