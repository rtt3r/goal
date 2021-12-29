using System;
using Goal.Application.Seedwork.Handlers;
using Goal.Demo2.Api.Application.CommandHandlers;
using Goal.Demo2.Api.Application.Commands.Customers;
using Goal.Demo2.Api.Application.EventHandlers;
using Goal.Demo2.Api.Application.Events;
using Goal.Demo2.Api.Infra.Bus;
using Goal.Demo2.Infra.Data;
using Goal.Demo2.Infra.Data.EventSourcing;
using Goal.Demo2.Infra.Data.Repositories;
using Goal.Domain.Seedwork;
using Goal.Domain.Seedwork.Aggregates;
using Goal.Domain.Seedwork.Events;
using Goal.Infra.Http.Seedwork.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goal.Demo2.Api.Infra.Extensions
{
    public static class DependencyInjectionMethods
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddHttpContextAccessor();
            //services.AddScoped<ElasticAuditChangesInterceptor>();

            services
                .AddDbContext<EventSourcingContext>((provider, options) =>
                {
                    options
                        .UseSqlite(
                            connectionString,
                            opts => opts.MigrationsAssembly(typeof(EventSourcingContext).Assembly.GetName().Name))
                        .EnableSensitiveDataLogging();

                    //options.AddInterceptors(provider.GetRequiredService<ElasticAuditChangesInterceptor>());
                });

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

            // Domain Bus (Mediator)
            services.AddScoped<IBusHandler, InMemoryBusHandler>();

            // Domain Event Store
            services.AddScoped<IEventStore, SqlEventStore>();

            // Domain - Events
            services.AddScoped<INotificationHandler, NotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, Guid>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, bool>, CustomerCommandHandler>();

            services.AddScoped<IUnitOfWork, Demo2UnitOfWork>();

            services.RegisterAllTypesOf<IRepository>(typeof(CustomerRepository).Assembly);

            return services;
        }
    }
}
