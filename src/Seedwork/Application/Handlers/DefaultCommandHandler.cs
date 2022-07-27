using System.Threading.Tasks;
using Goal.Seedwork.Domain.Commands;
using MediatR;

namespace Goal.Seedwork.Application.Handlers
{
    public sealed class DefaultCommandHandler : ICommandHandler
    {
        private readonly IMediator mediator;

        public DefaultCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task<ICommandResult<TResult>> HandleCommand<TCommand, TResult>(TCommand command)
            where TCommand : ICommand<ICommandResult<TResult>>
            => mediator.Send(command);

        public Task<ICommandResult> HandleCommand<TCommand>(TCommand command)
            where TCommand : ICommand<ICommandResult>
            => mediator.Send(command);
    }
}
