using Goal.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Trying;

public class UnitType_ImplicitOperator
{
    [Fact]
    public void Option_To_UnitType_Convertion_Should_Return_Unit()
    {
        // Arrange
        var option = Option.Of(42);

        // Act
        UnitType unitType = option;

        // Assert
        Assert.Equal(option, unitType);
    }
}