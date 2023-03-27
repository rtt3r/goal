using System.Reflection;
using FluentAssertions;
using Goal.Seedwork.Infra.Http.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.DependencyInjection
{
    public class ServiceCollectionExtensions_RegisterAllTypesOf
    {
        [Fact]
        public void Should_RegisterServices_GivenAssembly()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            var assembly = Assembly.GetExecutingAssembly();
            serviceCollection.RegisterAllTypesOf<IExampleService>(assembly);

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
            serviceCollection.RegisterAllTypesOf<IExampleService>();

            // Assert
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<IExampleService>().Should().NotBeNull();
        }

        public interface IExampleService { }
        public class ExampleService : IExampleService { }
    }
}
