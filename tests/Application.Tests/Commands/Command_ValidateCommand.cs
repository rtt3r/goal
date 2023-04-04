using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Goal.Seedwork.Application.Commands;
using Xunit;

namespace Goal.Seedwork.Application.Tests.Commands;

public class Command_ValidateCommand
{
    [Fact]
    public async Task ShouldReturnValidResult_IfNoValidationErrors()
    {
        // Arrange
        var command = new TestCommand { Prop1 = "value", Prop2 = 123 };
        var validator = new TestValidator();

        // Act
        FluentValidation.Results.ValidationResult result = await command.ValidateCommandAsync(validator);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldReturnInvalidResult_IfValidationErrorsExist()
    {
        // Arrange
        var command = new TestCommand { Prop1 = "", Prop2 = -1 };
        var validator = new TestValidator();

        // Act
        FluentValidation.Results.ValidationResult result = await command.ValidateCommandAsync(validator);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public async Task ShouldReturnValidResult_IfNoValidationErrors_Generic()
    {
        // Arrange
        var command = new TestCommand { Prop1 = "value", Prop2 = 123 };

        // Act
        FluentValidation.Results.ValidationResult result = await command.ValidateCommandAsync<TestValidator, TestCommand>();

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldReturnInvalidResult_IfValidationErrorsExist_Generic()
    {
        // Arrange
        var command = new TestCommand { Prop1 = "", Prop2 = -1 };

        // Act
        FluentValidation.Results.ValidationResult result = await command.ValidateCommandAsync<TestValidator, TestCommand>();

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    private class TestCommand : ICommand
    {
        public string Prop1 { get; set; }
        public int Prop2 { get; set; }
    }

    private class TestValidator : AbstractValidator<TestCommand>
    {
        public TestValidator()
        {
            RuleFor(c => c.Prop1)
                .NotEmpty();

            RuleFor(c => c.Prop2)
                .GreaterThan(0);
        }
    }
}
