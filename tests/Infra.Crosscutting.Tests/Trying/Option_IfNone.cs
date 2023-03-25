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
            bool ranAction = false;
            Option<int> option = isNone ? Option.None() : Option.Of(123);
            Option<int> result = option.IfNone(() => ranAction = true);
            result.IsNone.Should().Be(isNone);
            ranAction.Should().Be(isNone);
        }
    }
}