namespace Shared.Abstractions.Mediation.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
    }
}
