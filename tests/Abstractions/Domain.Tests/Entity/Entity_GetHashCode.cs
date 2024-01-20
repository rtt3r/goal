using FluentAssertions;
using Goal.Domain.Aggregates;
using Xunit;

namespace Goal.Domain.Tests.Entity;

public class Entity_GetHashCode
{
    private class TestEntity : Entity<int>
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
