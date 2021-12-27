using FluentAssertions;
using Goal.Domain.Aggregates;
using Goal.Domain.Seedwork.Tests.Mocks;
using Xunit;

namespace Goal.Domain.Seedwork.Tests.Entity
{
    public class Entity_Equals
    {
        [Fact]
        public void ReturnFalseGivenNotEntity()
        {
            //Given
            IEntity entity = new EntityTest(3);
            object obj = new();

            //When
            bool areEquals = entity.Equals(obj);

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueGivenIntransientSameReference()
        {
            //Given
            IEntity entity1 = new EntityTest(3);
            IEntity entity2 = entity1;

            //When
            bool areEquals = entity1.Equals(entity2);

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseGivenLeftTransient()
        {
            //Given
            IEntity entity1 = new EntityTest();
            IEntity entity2 = new EntityTest(3);

            //When
            bool areEquals = entity1.Equals(entity2);

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseGivenRightTransient()
        {
            //Given
            IEntity entity1 = new EntityTest(3);
            IEntity entity2 = new EntityTest();

            //When
            bool areEquals = entity1.Equals(entity2);

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueGivenBothIntransient()
        {
            //Given
            IEntity entity1 = new EntityTest(3);
            IEntity entity2 = new EntityTest(3);

            //When
            bool areEquals = entity1.Equals(entity2);

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseGivenBothIntransient()
        {
            //Given
            IEntity entity1 = new EntityTest(3);
            IEntity entity2 = new EntityTest(4);

            //When
            bool areEquals = entity1.Equals(entity2);

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueGivenBothTransientSameReference()
        {
            //Given
            var entity1 = new EntityTest();
            EntityTest entity2 = entity1;

            //When
            bool areEquals = entity1.Equals(entity2);

            //Then
            areEquals.Should().BeTrue();
        }
    }
}
