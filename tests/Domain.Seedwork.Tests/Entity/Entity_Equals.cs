using System;
using FluentAssertions;
using Goal.Domain.Seedwork.Aggregates;
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
            IEntity entity = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
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
            IEntity entity1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
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
            IEntity entity2 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));

            //When
            bool areEquals = entity1.Equals(entity2);

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseGivenRightTransient()
        {
            //Given
            IEntity entity1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
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
            IEntity entity1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            IEntity entity2 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));

            //When
            bool areEquals = entity1.Equals(entity2);

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseGivenBothIntransient()
        {
            //Given
            IEntity entity1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            IEntity entity2 = new EntityTest(Guid.Parse("09927327-c55b-4c17-8171-ca32131f4357"));

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
