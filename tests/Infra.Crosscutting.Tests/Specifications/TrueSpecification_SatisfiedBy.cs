using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Specifications;
using Goal.Seedwork.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Specifications
{
    public class TrueSpecification_SatisfiedBy
    {
        [Fact]
        public void GivenTransientEntityThenSatisfySpecification()
        {
            Specification<TestObject1> spec1 = new TrueSpecification<TestObject1>();
            spec1.IsSatisfiedBy(new TestObject1()).Should().BeTrue();
        }
    }
}
