using System.Reflection;
using FluentAssertions;
using Goal.Seedwork.Infra.Http.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.DependencyInjection;

public class ServiceCollectionExtensions_RegisterAllTypes
{
    [Fact]
    public void Should_RegisterServices_Given_Assembly()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var assembly = Assembly.GetExecutingAssembly();

        // Act
        serviceCollection.RegisterAllTypes(typeof(IExampleService), assembly);

        // Assert
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        serviceProvider.GetService<IExampleService>().Should().NotBeNull();
    }

    [Fact]
    public void Should_RegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.RegisterAllTypes(typeof(IExampleService));

        // Assert
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        serviceProvider.GetService<IExampleService>().Should().NotBeNull();
    }

    public interface IExampleService { }
    public class ExampleService : IExampleService { }
}
