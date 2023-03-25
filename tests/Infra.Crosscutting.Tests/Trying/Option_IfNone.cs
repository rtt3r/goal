using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_IfNone
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ActionIsCalledIfOptionIsNone(bool isNone)
        {
            var ranAction = false;
            var option = isNone ? Helpers.None : Helpers.Some(123);
            var result = option.IfNone(() => ranAction = true);
            result.IsNone.Should().Be(isNone);
            ranAction.Should().Be(isNone);
        }
    }
}