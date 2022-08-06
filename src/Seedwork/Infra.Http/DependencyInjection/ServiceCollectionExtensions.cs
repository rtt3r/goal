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
