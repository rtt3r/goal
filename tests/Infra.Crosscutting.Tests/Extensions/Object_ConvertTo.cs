using FluentAssertions;
using Vantage.Infra.Crosscutting.Extensions;
using Xunit;

namespace Vantage.Infra.Crosscutting.Tests.Extensions
{
    public class Object_ConvertTo
    {
        [Fact]
        public void ConvertNumbersSuccessullyGivenValidValue()
        {
            short shortValue = 3;

            int intValue = shortValue.ConvertTo<int>();
            intValue.Should().Be(3).And.BeOfType(typeof(int));

            long longValue = intValue.ConvertTo<long>();
            longValue.Should().Be(3).And.BeOfType(typeof(long));
        }
    }
}
