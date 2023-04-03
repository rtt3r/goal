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
    public void GetHashCode_Intransient_ReturnsNewHash()
    {
        //Given
        var entity = new EntityTest();

        //When
        int currentHash = entity.GetHashCode();
        entity.SetId(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
        int newHash = entity.GetHashCode();

        //Then
        newHash.Should().NotBe(currentHash);
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
