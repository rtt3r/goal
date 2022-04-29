using System;
using FluentAssertions;
using Goal.Seedwork.Domain.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Domain.Tests.Entity
{
    public class Entity_InequalityOperator
    {
        [Fact]
        public void ReturnTrueGivenNotNullObjects()
        {
            //Given
            var obj1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            var obj2 = new EntityTest(Guid.Parse("76da756f-860a-4235-921f-90895a5643d1"));

            //When
            bool areEquals = obj1 != obj2;

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseGivenNotNullObjects()
        {
            //Given
            var obj1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            var obj2 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));

            //When
            bool areEquals = obj1 != obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueGivenNullLeftObjects()
        {
            //Given
            EntityTest obj1 = null;
            var obj2 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));

            //When
            bool areEquals = obj1 != obj2;

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void ReturnTrueGivenNullRightObjects()
        {
            //Given
            var obj1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            EntityTest obj2 = null;

            //When
            bool areEquals = obj1 != obj2;

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseGivenNullBothObjects()
        {
            //Given
            EntityTest obj1 = null;
            EntityTest obj2 = null;

            //When
            bool areEquals = obj1 != obj2;

            //Then
            areEquals.Should().BeFalse();
        }
    }
}
