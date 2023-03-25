using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Helpers_ToFunc
    {
        [Fact]
        public void Action_ShouldInvokePassedAction()
        {
            // Arrange 
            bool invoked = false;
            void action() => invoked = true;

            // Act
            var func = Helpers.ToFunc(action);
            Unit result = func();

            // Assert
            invoked.Should().BeTrue();
            result.Should().NotBeNull();
        }

        [Fact]
        public void ActionT_ShouldInvokePassedAction()
        {
            // Arrange 
            bool invoked = false;
            void action(int _) => invoked = true;

            // Act
            var func = Helpers.ToFunc((Action<int>)action);
            Unit result = func(42);

            // Assert
            invoked.Should().BeTrue();
            result.Should().NotBeNull();
        }
    }
}