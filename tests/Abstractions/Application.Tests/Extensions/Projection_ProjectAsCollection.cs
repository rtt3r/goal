using System.Collections.Generic;
using FluentAssertions;
using Goal.Application.Extensions;
using Goal.Infra.Crosscutting.Adapters;
using Moq;
using Xunit;

namespace Goal.Application.Tests.Extensions;

public class Projection_ProjectAsCollection
{
    [Fact]
    public void AdaptCollectionGivenSourceAndTarget()
    {
        var mockAdapter = new Mock<ITypeAdapter>();
        mockAdapter.Setup(p => p.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>())).Returns([new TestMapClass()]);

        ICollection<TestMapClass> adapted = mockAdapter.Object.AdaptList<TestMapClass>(It.IsAny<IEnumerable<TestClass>>());

        mockAdapter.Verify(x => x.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>()), Times.Once);
        adapted.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void AdaptCollectionGivenTarget()
    {
        var mockAdapter = new Mock<ITypeAdapter>();
        mockAdapter.Setup(p => p.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>())).Returns([new TestMapClass()]);

        ICollection<TestMapClass> adapted = mockAdapter.Object.AdaptList<TestMapClass>(It.IsAny<IEnumerable<TestClass>>());

        mockAdapter.Verify(x => x.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>()), Times.Once);
        adapted.Should().NotBeNullOrEmpty();
    }

    internal class TestClass
    {
        public string? Test { get; set; }
    }

    internal class TestMapClass
    {
        public string? Test { get; set; }
    }
}
