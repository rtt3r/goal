using FluentAssertions;
using Goal.Seedwork.Infra.Data.Query;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.Repositories
{
    public class Repository_Dispose
    {
        [Fact]
        public void Dispose_Calls_DisposeImplementation()
        {
            // Arrange
            var repository = new MockQueryRepository();

            // Act
            repository.Dispose();

            // Assert
            repository.Disposed.Should().BeTrue();
        }

        private class MockQueryRepository : QueryRepository
        {
            public bool Disposed { get; private set; }

            protected override void Dispose(bool disposing)
            {
                if (disposing && !Disposed)
                {
                    Disposed = true;
                }
            }
        }
    }
}
