using System.Globalization;
using FluentAssertions;
using Goal.Infra.Crosscutting.Extensions;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Extensions;

public class CultureInfo_IsEqual
{
    [Fact]

    public void ReturnTrueGivenEqualCultures()
    {
        var culture1 = new CultureInfo("pt-BR");
        var culture2 = new CultureInfo("pt-BR");

        bool isEqual = culture1.IsEqual(culture2);

        isEqual.Should().BeTrue();
    }

    [Fact]

    public void ReturnFalseGivenNotEqualCultures()
    {
        var culture1 = new CultureInfo("pt-BR");
        var culture2 = new CultureInfo("en-US");

        bool isEqual = culture1.IsEqual(culture2);

        isEqual.Should().BeFalse();
    }

    [Fact]
    public void IsEqual_ReturnsTrue_WhenCulturesHaveSameCompareInfo()
    {
        // Arrange
        var culture1 = new CultureInfo("en-US");
        var culture2 = new CultureInfo("EN-us");

        // Act
        bool result = culture1.IsEqual(culture2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsEqual_ReturnsFalse_WhenCulturesHaveDifferentCompareInfo()
    {
        // Arrange
        var culture1 = new CultureInfo("en-US");
        var culture2 = new CultureInfo("fr-FR");

        // Act
        bool result = culture1.IsEqual(culture2);

        // Assert
        result.Should().BeFalse();
    }
}
