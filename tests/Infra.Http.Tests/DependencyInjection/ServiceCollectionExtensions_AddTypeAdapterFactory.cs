using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Adapters;
using Goal.Seedwork.Infra.Http.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.DependencyInjection;

public class ServiceCollectionExtensions_AddTypeAdapterFactory
{
    [Fact]
    public void AddsSingleton_ITypeAdapterFactory_WithSpecifiedImplementation()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.AddTypeAdapterFactory<CustomTypeAdapterFactory>();
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        serviceProvider.GetService<ITypeAdapterFactory>().Should().NotBeNull();
        serviceProvider.GetService<ITypeAdapterFactory>().Should().BeOfType<CustomTypeAdapterFactory>();
    }

    [Fact]
    public void AddsSingleton_InstanceCreated_ByCallingCreateMethod()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var typeAdapterFactory = new CustomTypeAdapterFactory();

        // Act
        serviceCollection.AddTypeAdapterFactory(typeAdapterFactory);
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        serviceProvider.GetService<ITypeAdapterFactory>().Should().NotBeNull();
        serviceProvider.GetService<ITypeAdapterFactory>().Should().BeOfType<CustomTypeAdapterFactory>();
        serviceProvider.GetService<ITypeAdapterFactory>().Should().BeSameAs(typeAdapterFactory);
    }

    private class CustomTypeAdapterFactory : ITypeAdapterFactory
    {
        public ITypeAdapter Create() => null;
    }
}
