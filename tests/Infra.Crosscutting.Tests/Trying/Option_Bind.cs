using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying;

public class Option_Bind
{
    [Fact]
    public void WithSome_ReturnsOptionFromBindFunction()
    {
        var option = Option.Of(123);
        Option<string> result = option.Bind(x => Option.Of("hi"));
        result.IsSome.Should().BeTrue();
        result.Value.Should().Be("hi");
    }

    [Fact]
    public void WithNone_ReturnsNone()
    {
        var option = Option.Of<object>(null!);
        Option<string> result = option.Bind(x => Option.Of("hi"));
        result.IsNone.Should().BeTrue();
    }
}