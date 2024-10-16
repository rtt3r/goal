using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Goal.Application.Commands;

namespace Goal.Application.Extensions;

public static class CommandsExtensionsMethods
{
    public static async Task<ValidationResult> ValidateCommandAsync<TValidator, TCommand>(
        this TCommand command,
        CancellationToken cancellationToken = new CancellationToken())
        where TValidator : IValidator<TCommand>, new()
        where TCommand : ICommand
        => await new TValidator().ValidateAsync(command, cancellationToken);

    public static async Task<ValidationResult> ValidateCommandAsync<TCommand>(
        this TCommand command,
        IValidator<TCommand> validator,
        CancellationToken cancellationToken = new CancellationToken())
        where TCommand : ICommand
        => await validator.ValidateAsync(command, cancellationToken);
}
