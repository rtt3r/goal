using FluentAssertions;
using Goal.Infra.Crosscutting.Adapters;
using Moq;
using Xunit;

namespace Goal.Application.Tests.Extensions;

public class Projection_ProjectAs
{
    [Fact]
    public void AdaptClassGivenSourceAndTarget()
    {
        var mockAdapter = new Mock<ITypeAdapter>();
        mockAdapter.Setup(p => p.Adapt<TestClass, TestMapClass>(It.IsAny<TestClass>())).Returns(new TestMapClass(""));

        TestMapClass adapted = mockAdapter.Object.Adapt<TestClass, TestMapClass>(It.IsAny<TestClass>());

        mockAdapter.Verify(x => x.Adapt<TestClass, TestMapClass>(It.IsAny<TestClass>()), Times.Once);
        adapted.Should().NotBeNull();
    }

    [Fact]
    public void AdaptClassGivenTarget()
    {
        var mockAdapter = new Mock<ITypeAdapter>();
        mockAdapter.Setup(p => p.Adapt<TestMapClass>(It.IsAny<TestClass>())).Returns(new TestMapClass(""));

        TestMapClass adapted = mockAdapter.Object.Adapt<TestMapClass>(It.IsAny<TestClass>());

        mockAdapter.Verify(x => x.Adapt<TestMapClass>(It.IsAny<TestClass>()), Times.Once);
        adapted.Should().NotBeNull();
    }

    internal record TestClass(string? Test);

    internal record TestMapClass(string? Test);
}
