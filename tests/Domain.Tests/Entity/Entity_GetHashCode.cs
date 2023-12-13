using System;
using FluentAssertions;
using Goal.Seedwork.Domain.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Domain.Tests.Entity;

public class Entity_GetHashCode
{
    private class TestEntity : Domain.Aggregates.Entity<int>
    {
        public TestEntity(int id)
        {
            Id = id;
        }
    }

    [Fact]
    public void GetHashCode_SameTypeAndId_ReturnsSameHash()
    {
        // Arrange
        var entity1 = new TestEntity(1);
        var entity2 = new TestEntity(1);

        // Act
        int hash1 = entity1.GetHashCode();
        int hash2 = entity2.GetHashCode();

        // Assert
        hash1.Should().Be(hash2);
    }
}
