using System.Collections.Generic;
using FluentAssertions;
using Goal.Application.Extensions;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Collections;
using Moq;
using Xunit;

namespace Goal.Application.Tests.Extensions;

public class Projection_ProjectAsPagedCollection
{
    [Fact]
    public void AdaptPagedCollectionGivenSourceAndTarget()
    {
        var mockAdapter = new Mock<ITypeAdapter>();
        mockAdapter.Setup(p => p.Adapt<IEnumerable<TestClass>, List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>())).Returns([new TestMapClass("")]);

        IPagedList<TestMapClass> adapted = mockAdapter.Object.AdaptPagedList<TestClass, TestMapClass>(new PagedList<TestClass>([new TestClass("")], 1));

        mockAdapter.Verify(x => x.Adapt<IEnumerable<TestClass>, List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>()), Times.Once);
        adapted.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void AdaptPagedCollectionGivenTarget()
    {
        var mockAdapter = new Mock<ITypeAdapter>();
        mockAdapter.Setup(p => p.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>())).Returns([new TestMapClass("")]);

        IPagedList<TestMapClass> adapted = mockAdapter.Object.AdaptPagedList<TestMapClass>(new PagedList<TestClass>([new TestClass("")], 1));

        mockAdapter.Verify(x => x.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>()), Times.Once);
        adapted.Should().NotBeNullOrEmpty();
    }

    internal record TestClass(string? Test);

    internal record TestMapClass(string? Test);
}
