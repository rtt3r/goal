using System;
using FluentAssertions;
using Goal.Domain.Seedwork.Tests.Mocks;
using Xunit;

namespace Goal.Domain.Seedwork.Tests.Entity
{
    public class Entity_EqualityOperator
    {
        [Fact]
        public void ReturnFalseGivenNotNullObjects()
        {
            //Given
            var obj1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            var obj2 = new EntityTest(Guid.Parse("9ae0bc81-9586-4c47-b695-b59a6f584bd2"));

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueGivenNotNullObjects()
        {
            //Given
            var obj1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            var obj2 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseGivenNullLeftObjects()
        {
            //Given
            EntityTest obj1 = null;
            var obj2 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseGivenNullRightObjects()
        {
            //Given
            var obj1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            EntityTest obj2 = null;

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueGivenNullBothObjects()
        {
            //Given
            EntityTest obj1 = null;
            EntityTest obj2 = null;

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeTrue();
        }
    }
}
