using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Goal.Seedwork.Infra.Http.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.DependencyInjection
{
    public class ServiceCollectionExtensions_AddDefaultNotificationHandler
    {
        [Fact]
        public void AddsDefaultNotificationHandler()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddDefaultNotificationHandler();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // Assert
            serviceProvider.GetService<IDefaultNotificationHandler>().Should().NotBeNull();
            serviceProvider.GetService<IDefaultNotificationHandler>().Should().BeOfType<DefaultNotificationHandler>();
        }

        [Fact]
        public void AddsDefaultNotificationHandler_WithSpecifiedImplementation()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddNotificationHandler<CustomNotificationHandler>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // Assert
            serviceProvider.GetService<IDefaultNotificationHandler>().Should().NotBeNull();
            serviceProvider.GetService<IDefaultNotificationHandler>().Should().BeOfType<CustomNotificationHandler>();
        }

        [Fact]
        public void AddsDefaultNotificationHandler_WithSpecifiedImplementationInstance()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var notificationHandler = new CustomNotificationHandler();

            // Act
            serviceCollection.AddNotificationHandler(notificationHandler);
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // Assert
            serviceProvider.GetService<IDefaultNotificationHandler>().Should().NotBeNull();
            serviceProvider.GetService<IDefaultNotificationHandler>().Should().BeOfType<CustomNotificationHandler>();
            serviceProvider.GetService<IDefaultNotificationHandler>().Should().BeSameAs(notificationHandler);
        }

        public class CustomNotificationHandler : NotificationHandler<Notification>, IDefaultNotificationHandler { }
    }
}
