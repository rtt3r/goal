using FluentAssertions;
using Goal.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Trying;

public class Option_None
{
    [Fact]
    public void ShouldReturnNonNullUnit()
    {
        // Act
        NoneType result = Option.None();

        // Assert
        result.Should().NotBeNull();
    }
}