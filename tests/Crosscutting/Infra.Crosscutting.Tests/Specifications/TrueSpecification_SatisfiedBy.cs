using FluentAssertions;
using Goal.Infra.Crosscutting.Specifications;
using Goal.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Specifications;

public class TrueSpecification_SatisfiedBy
{
    [Fact]
    public void GivenTransientEntityThenSatisfySpecification()
    {
        Specification<TestObject1> spec1 = new TrueSpecification<TestObject1>();
        spec1.IsSatisfiedBy(new TestObject1()).Should().BeTrue();
    }
}
