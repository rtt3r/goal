using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Specifications;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Specifications
{
    public class ParameterRebinder_ReplaceParameters
    {
        [Fact]
        public void ShouldSubstituteAllDefinedParameters()
        {
            // Arrange
            ParameterExpression originalParam1 = Expression.Parameter(typeof(decimal), "originalParam1");
            ParameterExpression originalParam2 = Expression.Parameter(typeof(decimal), "originalParam2");
            ParameterExpression newParam1 = Expression.Parameter(typeof(decimal), "newParam1");
            ParameterExpression newParam2 = Expression.Parameter(typeof(decimal), "newParam2");

            var map = new Dictionary<ParameterExpression, ParameterExpression>
            {
                { originalParam1, newParam1 },
                { originalParam2, newParam2 }
            };

            ConstantExpression constantExpr = Expression.Constant(123M);
            BinaryExpression addExpr = Expression.Add(originalParam1, constantExpr);

            MethodCallExpression concatExpr = Expression.Call(
                typeof(decimal).GetMethod("Add", new[] { typeof(decimal), typeof(decimal) }),
                addExpr,
                originalParam2);

            // Act
            Expression result = ParameterRebinder.ReplaceParameters(map, concatExpr);

            // Assert
            var lambda = Expression.Lambda<Func<decimal, decimal, decimal>>(result, newParam1, newParam2);
            Func<decimal, decimal, decimal> compiledLambda = lambda.Compile();

            decimal expected = 162M;
            decimal actual = compiledLambda(14M, 25M);

            actual.Should().Be(expected);
        }

        [Fact]
        public void ShouldNotSubstituteUndefinedParameters()
        {
            // Arrange
            ParameterExpression originalParam1 = Expression.Parameter(typeof(int), "originalParam1");
            ParameterExpression originalParam2 = Expression.Parameter(typeof(int), "originalParam2");

            var map = new Dictionary<ParameterExpression, ParameterExpression>
            {
                { originalParam2, originalParam1 }
            };

            // Act
            Expression result = ParameterRebinder.ReplaceParameters(map, originalParam2);

            // Assert
            var lambda = Expression.Lambda<Func<int, int>>(result, originalParam1);
            Func<int, int> compiledLambda = lambda.Compile();

            int expected = 246;
            int actual = compiledLambda(expected);

            actual.Should().Be(expected);
        }
    }

}