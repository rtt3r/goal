using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Repositories
{
    public class Repository_Dispose
    {
        [Fact]
        public void CallsProtectedDisposeMethodWithTrue()
        {
            // Arrange
            var repository = new TestRepository();

            // Act
            repository.Dispose();

            // Assert
            repository.IsDisposed.Should().BeTrue();
        }

        private class TestRepository : Repository
        {
            public bool IsDisposed { get; private set; }

            protected override void Dispose(bool disposing)
            {
                // Do actual disposal here
                IsDisposed = true;
            }
        }
    }
}
