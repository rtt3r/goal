using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_None
    {
        [Fact]
        public void ShouldReturnNonNullUnit()
        {
            // Act
            var result = Option.None();

            // Assert
            result.Should().NotBeNull();
        }
    }
}