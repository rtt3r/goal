using Goal.Infra.Crosscutting;
using Goal.Infra.Crosscutting.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Goal.Infra.Http.Seedwork.Extensions
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
    }
}
