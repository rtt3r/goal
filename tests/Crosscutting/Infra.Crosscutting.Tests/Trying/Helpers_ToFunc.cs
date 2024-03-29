using System;
using FluentAssertions;
using Goal.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Trying;

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
        UnitType result = func();

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
        UnitType result = func(42);

        // Assert
        invoked.Should().BeTrue();
        result.Should().NotBeNull();
    }
}