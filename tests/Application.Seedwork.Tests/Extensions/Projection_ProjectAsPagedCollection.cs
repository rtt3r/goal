using System.Collections.Generic;
using FluentAssertions;
using Goal.Application.Extensions;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Collections;
using Moq;
using Xunit;

namespace Goal.Application.Seedwork.Tests.Extensions
{
    public class Projection_ProjectAsPagedCollection
    {
        [Fact]
        public void AdaptPagedCollectionGivenSourceAndTarget()
        {
            var mockAdapter = new Mock<ITypeAdapter>();
            mockAdapter.Setup(p => p.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>())).Returns(new List<TestMapClass> { new TestMapClass() });

            IPagedCollection<TestMapClass> adapted = mockAdapter.Object.ProjectAsPagedCollection<TestClass, TestMapClass>(new PagedList<TestClass>(new[] { new TestClass() }, 1));

            mockAdapter.Verify(x => x.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>()), Times.Once);
            adapted.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void AdaptPagedCollectionGivenTarget()
        {
            var mockAdapter = new Mock<ITypeAdapter>();
            mockAdapter.Setup(p => p.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>())).Returns(new List<TestMapClass> { new TestMapClass() });

            IPagedCollection<TestMapClass> adapted = mockAdapter.Object.ProjectAsPagedCollection<TestMapClass>(new PagedList<TestClass>(new[] { new TestClass() }, 1));

            mockAdapter.Verify(x => x.Adapt<List<TestMapClass>>(It.IsAny<IEnumerable<TestClass>>()), Times.Once);
            adapted.Should().NotBeNullOrEmpty();
        }

        internal class TestClass
        {
            public string Test { get; set; }
        }

        internal class TestMapClass
        {
            public string Test { get; set; }
        }
    }
}
