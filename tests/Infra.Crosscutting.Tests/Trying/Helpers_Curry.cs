using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying;

public class Helpers_Curry
{
    [Fact]
    public void With_2_Parameters_Should_Return_Working_Function()
    {
        // Arrange
        Func<int, Func<int, int>> sum = Helpers.Curry<int, int, int>((a, b) => a + b);

        // Act
        int result = sum(2)(3);

        // Assert
        result.Should().Be(5);
    }
}