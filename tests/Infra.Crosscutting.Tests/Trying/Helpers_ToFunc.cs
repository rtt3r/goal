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
            Action action = () => invoked = true;

            // Act
            var func = Helpers.ToFunc(action);
            var result = func();

            // Assert
            invoked.Should().BeTrue();
            result.Should().NotBeNull();
        }

        [Fact]
        public void ActionT_ShouldInvokePassedAction()
        {
            // Arrange 
            bool invoked = false;
            Action<int> action = (_) => invoked = true;

            // Act
            var func = Helpers.ToFunc(action);
            var result = func(42);

            // Assert
            invoked.Should().BeTrue();
            result.Should().NotBeNull();
        }
    }
}