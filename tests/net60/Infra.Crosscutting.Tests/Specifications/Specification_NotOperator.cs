using FluentAssertions;
using Vantage.Infra.Crosscutting.Specifications;
using Vantage.Infra.Crosscutting.Tests.Mocks;
using System;
using Xunit;

namespace Vantage.Infra.Crosscutting.Tests.Specifications
{
    public class Specification_NotOperator
    {
        [Fact]
        public void GivenTransientEntityThenNotSatisfySpecification()
        {
            var spec2 = new DirectSpecification<TestObject1>(e => e.Id == 0);

            var notSpec = !spec2;

            notSpec.Should().BeOfType<NotSpecification<TestObject1>>();
            notSpec.IsSatisfiedBy(new TestObject1()).Should().BeFalse();
        }

        [Fact]
        public void GivenNotTransientEntityThenSatisfySpecification()
        {
            var spec2 = new DirectSpecification<TestObject1>(e => e.Id == 0);

            var notSpec = !spec2;

            notSpec.Should().BeOfType<NotSpecification<TestObject1>>();
            notSpec.IsSatisfiedBy(new TestObject1(1)).Should().BeTrue();
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenNullSpecification()
        {
            Specification<TestObject1> spec2 = null;

            Action act = () =>
            {
                var notSpec = !spec2;
            };

            act.Should().Throw<ArgumentNullException>().And.Message.Should().Be("Object value cannot be null (Parameter 'originalSpecification')");
        }
    }
}
