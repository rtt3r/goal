using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Helpers_Unit
    {
        [Fact]
        public void ShouldReturnNonNullUnit()
        {
            // Act
            var result = Helpers.Unit();

            // Assert
            result.Should().NotBeNull();
        }
    }
}