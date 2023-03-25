using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_Unit
    {
        [Fact]
        public void ShouldReturnNonNullUnit()
        {
            // Act
            UnitType result = Option.Unit();

            // Assert
            result.Should().NotBeNull();
        }
    }
}