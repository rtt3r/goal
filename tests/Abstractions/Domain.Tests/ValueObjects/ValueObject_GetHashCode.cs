using FluentAssertions;
using Goal.Seedwork.Domain.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Domain.Tests.ValueObjects;

public class ValueObject_GetHashCode
{
    [Fact]
    public void GetHashGivenObjectWithNotNullProperties()
    {
        //Given
        var obj1 = new ValueObject1()
        {
            Id = 1,
            Value = "test"
        };

        //When
        int hash = obj1.GetHashCode();

        //Then
        hash.Should().NotBe(0);
    }

    [Fact]
    public void GetHashGivenObjectWithNullProperties()
    {
        //Given
        var obj1 = new ValueObject1()
        {
            Id = 1,
            Value = null
        };

        //When
        int hash = obj1.GetHashCode();

        //Then
        hash.Should().NotBe(0);
    }

    [Fact]
    public void GetHashGivenObjectWithoutProperties()
    {
        //Given
        var obj1 = new ValueObject2();

        //When
        int hash = obj1.GetHashCode();

        //Then
        hash.Should().NotBe(0);
    }
}
