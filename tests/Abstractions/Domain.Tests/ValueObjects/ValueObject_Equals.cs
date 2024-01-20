using FluentAssertions;
using Goal.Domain.Tests.Mocks;
using Xunit;

namespace Goal.Domain.Tests.ValueObjects;

public class ValueObject_Equals
{
    [Fact]
    public void ReturnFalseGivenNullObject()
    {
        var obj1 = new ValueObject1();
        object obj2 = null!;

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeFalse();
    }

    [Fact]
    public void ReturnTrueGivenObjectWithSameReference()
    {
        var obj1 = new ValueObject1 { Id = 1, Value = "value" };
        object obj2 = obj1;

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeTrue();
    }

    [Fact]
    public void ReturnFalseGivenOtherObjectType()
    {
        var obj1 = new ValueObject1 { Id = 1, Value = "value" };
        object obj2 = "Test";

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeFalse();
    }

    [Fact]
    public void ReturnTrueGivenOtherPropertyType()
    {
        var obj1 = new ValueObject3 { Id = 1, Value = "value", ValueObject = new ValueObject3() };
        var obj2 = new ValueObject3 { Id = 1, Value = "value", ValueObject = new ValueObject3() };

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeTrue();
    }

    [Fact]
    public void ReturnFalseGivenObjectAsValueObjectType()
    {
        var obj1 = new ValueObject1 { Id = 1, Value = "value" };
        object obj2 = new ValueObject1 { Id = 2, Value = "value2" };

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeFalse();
    }

    [Fact]
    public void ReturnFalseGivenNullValueObject()
    {
        var obj1 = new ValueObject1();
        ValueObject1 obj2 = null!;

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeFalse();
    }

    [Fact]
    public void ReturnFalseGivenValueObjectWithDifferentPropertyValues()
    {
        var obj1 = new ValueObject1 { Id = 1, Value = "value" };
        var obj2 = new ValueObject1 { Id = 2, Value = "value1" };

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeFalse();
    }

    [Fact]
    public void ReturnTrueGivenValueObjectWithSameReference()
    {
        var obj1 = new ValueObject1 { Id = 1, Value = "value" };
        ValueObject1 obj2 = obj1;

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeTrue();
    }

    [Fact]
    public void ReturnTrueGivenValueObjectWithoutProperties()
    {
        var obj1 = new ValueObject2();
        var obj2 = new ValueObject2();

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeTrue();
    }

    [Fact]
    public void ReturnTrueGivenValueObjectWithEqualsPropertyValues()
    {
        var obj1 = new ValueObject1 { Id = 1, Value = "value" };
        var obj2 = new ValueObject1 { Id = 1, Value = "value" };

        bool areEquals = obj1.Equals(obj2);

        areEquals.Should().BeTrue();
    }
}
