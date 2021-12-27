using System;
using FluentAssertions;
using Goal.Infra.Crosscutting.Specifications;
using Goal.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Specifications
{
    public class Specification_NotOperator
    {
        [Fact]
        public void GivenTransientEntityThenNotSatisfySpecification()
        {
            var spec2 = new DirectSpecification<TestObject1>(e => e.Id == 0);

            Specification<TestObject1> notSpec = !spec2;

            notSpec.Should().BeOfType<NotSpecification<TestObject1>>();
            notSpec.IsSatisfiedBy(new TestObject1()).Should().BeFalse();
        }

        [Fact]
        public void GivenNotTransientEntityThenSatisfySpecification()
        {
            var spec2 = new DirectSpecification<TestObject1>(e => e.Id == 0);

            Specification<TestObject1> notSpec = !spec2;

            notSpec.Should().BeOfType<NotSpecification<TestObject1>>();
            notSpec.IsSatisfiedBy(new TestObject1(1)).Should().BeTrue();
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenNullSpecification()
        {
            Specification<TestObject1> spec2 = null;

            Action act = () =>
            {
                Specification<TestObject1> notSpec = !spec2;
            };

            act.Should().Throw<ArgumentNullException>().And.Message.Should().Be("Object value cannot be null (Parameter 'originalSpecification')");
        }
    }
}
