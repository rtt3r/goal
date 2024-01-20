using System;
using System.Linq.Expressions;
using FluentAssertions;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Extensions;

public class Expression_GetPropertyName
{
    [Fact]
    public void ReturnPropertyNameGivenMemberExpression()
    {
        Expression<Func<TestObject1, string?>> expression = p => p.Value;
        string propertyName = expression.GetPropertyName();

        propertyName.Should().NotBeNull().And.Be("Value");
    }

    [Fact]
    public void ReturnPropertyNameGivenUnaryExpression()
    {
        Expression<Func<TestObject1, object>> expression = x => x.Id;
        string propertyName = expression.GetPropertyName();

        propertyName.Should().NotBeNull().And.Be("Id");
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenNullExpression()
    {
        Expression<Func<TestObject1, string>> expression = null!;
        Action act = () => expression.GetPropertyName();

        act.Should().Throw<ArgumentNullException>().WithParameterName("predicate");
    }

    [Fact]
    public void ThrowsArgumentExceptionGivenInvalidExpression()
    {
        Expression<Func<TestObject1, bool>> expression = p => p.Id == 0;
        Action act = () => expression.GetPropertyName();

        act.Should().Throw<ArgumentException>()
            .And.Message.Should().Be("Expression not supported. (Parameter 'predicate')");
    }
}
