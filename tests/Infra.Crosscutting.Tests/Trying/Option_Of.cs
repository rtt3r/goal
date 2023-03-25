using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_Of
    {
        [Fact]
        public void WithNonNullValue_ReturnsSome()
        {
            int value = 123;
            var result = Option.Of(value);
            result.IsSome.Should().BeTrue();
            result.Value.Should().Be(value);
        }

        [Fact]
        public void WithNullValue_ReturnsNone()
        {
            object value = null;
            var result = Option.Of(value);
            result.IsNone.Should().BeTrue();
        }
    }

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

    public class Option_Bind
    {
        [Fact]
        public void WithSome_ReturnsOptionFromBindFunction()
        {
            var option = Option.Of(123);
            var result = option.Bind(x => Option.Of("hi"));
            result.IsSome.Should().BeTrue();
            result.Value.Should().Be("hi");
        }

        [Fact]
        public void WithNone_ReturnsNone()
        {
            var option = Option.Of<object>(null);
            var result = option.Bind(x => Option.Of("hi"));
            result.IsNone.Should().BeTrue();
        }
    }

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