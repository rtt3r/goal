using System.Globalization;
using FluentAssertions;
using Vantage.Infra.Crosscutting.Extensions;
using Xunit;

namespace Vantage.Infra.Crosscutting.Tests.Extensions
{

    public class CultureInfo_IsEqual
    {
        [Fact]

        public void ReturnTrueGivenEqualCultures()
        {
            var culture1 = new CultureInfo("pt-BR");
            var culture2 = new CultureInfo("pt-BR");

            bool isEqual = culture1.IsEqual(culture2);

            isEqual.Should().BeTrue();
        }

        [Fact]

        public void ReturnFalseGivenNotEqualCultures()
        {
            var culture1 = new CultureInfo("pt-BR");
            var culture2 = new CultureInfo("en-US");

            bool isEqual = culture1.IsEqual(culture2);

            isEqual.Should().BeFalse();
        }
    }
}
