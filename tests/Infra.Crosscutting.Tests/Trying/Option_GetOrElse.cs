using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_GetOrElse
    {
        [Theory]
        [InlineData(42, true)]
        [InlineData("hello world", true)]
        [InlineData(null, false)]
        public void WithSome_ReturnsValue(object value, bool useFallback)
        {
            var option = Option.Of(value);

            if (useFallback)
            {
                var result = option.GetOrElse(() => 123);
                result.Should().Be(value);
            }
            else
            {
                var result = option.GetOrElse(456);
                result.Should().Be(456);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WithNone_ReturnsFallback(bool useDelegateFallback)
        {
            var option = Option.Of<int?>(null);

            if (useDelegateFallback)
            {
                var result = option.GetOrElse(() => 123);
                result.Should().Be(123);
            }
            else
            {
                var result = option.GetOrElse(456);
                result.Should().Be(456);
            }
        }
    }
}