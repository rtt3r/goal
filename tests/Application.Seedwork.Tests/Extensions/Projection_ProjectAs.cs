using FluentAssertions;
using Moq;
using Vantage.Application.Extensions;
using Vantage.Infra.Crosscutting.Adapters;
using Xunit;

namespace Vantage.Application.Seedwork.Tests.Extensions
{
    public class Projection_ProjectAs
    {
        [Fact]
        public void AdaptClassGivenSourceAndTarget()
        {
            var mockAdapter = new Mock<ITypeAdapter>();
            mockAdapter.Setup(p => p.Adapt<TestMapClass>(It.IsAny<TestClass>())).Returns(new TestMapClass());

            TestMapClass adapted = mockAdapter.Object.ProjectAs<TestClass, TestMapClass>(It.IsAny<TestClass>());

            mockAdapter.Verify(x => x.Adapt<TestMapClass>(It.IsAny<TestClass>()), Times.Once);
            adapted.Should().NotBeNull();
        }

        [Fact]
        public void AdaptClassGivenTarget()
        {
            var mockAdapter = new Mock<ITypeAdapter>();
            mockAdapter.Setup(p => p.Adapt<TestMapClass>(It.IsAny<TestClass>())).Returns(new TestMapClass());

            TestMapClass adapted = mockAdapter.Object.ProjectAs<TestMapClass>(It.IsAny<TestClass>());

            mockAdapter.Verify(x => x.Adapt<TestMapClass>(It.IsAny<TestClass>()), Times.Once);
            adapted.Should().NotBeNull();
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
