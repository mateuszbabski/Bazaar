namespace Shared.Abstractions.Mediation.Commands
{
    public interface ICommandHandler<in TCommand, TResult> 
        where TCommand : class, IRequest<TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
