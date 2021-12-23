using FluentAssertions;
using Vantage.Domain.Seedwork.Tests.Mocks;
using Xunit;

namespace Vantage.Domain.Seedwork.Tests.Entity
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
            entity.SetId(4);
            int newHash = entity.GetHashCode();

            //Then
            newHash.Should().NotBe(currentHash);
        }
    }
}
