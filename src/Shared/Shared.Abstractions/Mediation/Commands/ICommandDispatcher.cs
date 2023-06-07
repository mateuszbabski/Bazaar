namespace Shared.Abstractions.Mediation.Commands
{
    public interface ICommandDispatcher
    {
        //Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) 
        //    where TCommand : ICommand<TResult>;

        Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
    }
}
