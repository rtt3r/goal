using Goal.Seedwork.Application.Handlers;
using Goal.Seedwork.Infra.Crosscutting;
using Goal.Seedwork.Infra.Crosscutting.Adapters;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Goal.Seedwork.Infra.Http.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTypeAdapterFactory(this IServiceCollection services, ITypeAdapterFactory typeAdapterFactory)
        {
            Ensure.Argument.NotNull(typeAdapterFactory, nameof(typeAdapterFactory));

            services.AddSingleton(typeof(ITypeAdapterFactory), typeAdapterFactory);
            services.AddSingleton(factory => factory.GetService<ITypeAdapterFactory>().Create());

            return services;
        }

        public static IServiceCollection AddTypeAdapterFactory<TTypeAdapterFactory>(this IServiceCollection services)
            where TTypeAdapterFactory : class, ITypeAdapterFactory
        {
            services.AddSingleton<ITypeAdapterFactory, TTypeAdapterFactory>();
            services.AddSingleton(factory => factory.GetService<ITypeAdapterFactory>().Create());

            return services;
        }

        public static IServiceCollection AddCommandHandler(this IServiceCollection services, ICommandHandler commandHandler)
        {
            Ensure.Argument.NotNull(commandHandler, nameof(commandHandler));

            services.AddScoped(typeof(ICommandHandler), serviceProvider => commandHandler);
            return services;
        }

        public static IServiceCollection AddCommandHandler<TCommandHandler>(this IServiceCollection services)
            where TCommandHandler : class, ICommandHandler
        {
            services.AddScoped<ICommandHandler, TCommandHandler>();
            return services;
        }

        public static IServiceCollection AddDefaultCommandHandler(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler, DefaultCommandHandler>();
            return services;
        }

        public static IServiceCollection AddEventHandler(this IServiceCollection services, IEventHandler eventHandler)
        {
            Ensure.Argument.NotNull(eventHandler, nameof(eventHandler));

            services.AddScoped(typeof(IEventHandler), serviceProvider => eventHandler);
            return services;
        }

        public static IServiceCollection AddEventHandler<TEventHandler>(this IServiceCollection services)
            where TEventHandler : class, IEventHandler
        {
            services.AddScoped<IEventHandler, TEventHandler>();
            return services;
        }

        public static IServiceCollection AddDefaultEventHandler(this IServiceCollection services)
        {
            services.AddScoped<IEventHandler, DefaultEventHandler>();
            return services;
        }

        public static IServiceCollection AddNotificationHandler(this IServiceCollection services, INotificationHandler notificationHandler)
        {
            Ensure.Argument.NotNull(notificationHandler, nameof(notificationHandler));

            services.AddScoped(typeof(INotificationHandler), serviceProvider => notificationHandler);
            return services;
        }

        public static IServiceCollection AddNotificationHandler<TNotificationHandler>(this IServiceCollection services)
            where TNotificationHandler : class, INotificationHandler
        {
            services.AddScoped<INotificationHandler, TNotificationHandler>();
            return services;
        }

        public static IServiceCollection AddDefaultNotificationHandler(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler, DefaultNotificationHandler>();
            return services;
        }
    }
}
