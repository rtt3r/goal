using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_Map
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(4, 5, 9)]
        public void WithSome_ReturnsSome(int a, int b, int expected)
        {
            var option = Option.Of(a);
            var result = option.Map(x => x + b);
            result.IsSome.Should().BeTrue();
            result.Value.Should().Be(expected);
        }

        [Fact]
        public void WithNone_ReturnsNone()
        {
            var option = Option.Of<string>(null);
            var result = option.Map(x => x.ToUpper());
            result.IsNone.Should().BeTrue();
        }
    }
}