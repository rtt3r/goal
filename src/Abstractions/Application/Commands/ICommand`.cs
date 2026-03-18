using ConduitR.Abstractions;

namespace Goal.Application.Commands;

public interface ICommand<out TResult> : ICommand, IRequest<TResult>
{
}
