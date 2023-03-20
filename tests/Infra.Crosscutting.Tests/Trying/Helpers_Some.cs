using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Helpers_Some
    {
        [Fact]
        public void Creates_Option_Of_Some()
        {
            // Arrange & Act
            Option<int> option = Helpers.Some(10);

            // Assert
            option.IsSome.Should().BeTrue();
        }
    }
}