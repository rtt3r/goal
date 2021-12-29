using Goal.Application.Seedwork.Services;
using Goal.Demo2.Application.People;
using Goal.Demo2.Infra.Data;
using Goal.Demo2.Infra.Data.Repositories;
using Goal.Domain.Seedwork;
using Goal.Domain.Seedwork.Aggregates;
using Goal.Infra.Data.Seedwork;
using Goal.Infra.Http.Seedwork.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goal.Demo2.IoC
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddHttpContextAccessor();
            //services.AddScoped<ElasticAuditChangesInterceptor>();

            services
                .AddDbContext<Demo2Context>((provider, options) =>
                {
                    options
                        .UseSqlite(
                            connectionString,
                            opts => opts.MigrationsAssembly(typeof(Demo2Context).Assembly.GetName().Name))
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
