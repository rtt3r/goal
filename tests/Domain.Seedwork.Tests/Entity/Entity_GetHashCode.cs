using System;
using FluentAssertions;
using Goal.Domain.Seedwork.Tests.Mocks;
using Xunit;

namespace Goal.Domain.Seedwork.Tests.Entity
{
    public class Entity_GetHashCode
    {
        [Fact]
        public void ReturnNewHashGivenIntransient()
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
    }
}
