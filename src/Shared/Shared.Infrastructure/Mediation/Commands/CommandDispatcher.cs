using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Mediation.Commands;

namespace Shared.Infrastructure.Mediation.Commands
{
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        //public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) 
        //    where TCommand : ICommand<TResult>
        //{
        //    var handler = _serviceProvider.GetRequiredService<ICommandHandler<ICommand<TResult>, TResult>>();
        //    return await handler.HandleAsync(command, cancellationToken);
        //}

        public async Task<TResult> SendAsync<TResult>(IRequest<TResult> command, CancellationToken cancellationToken = default)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<IRequest<TResult>, TResult>>();
            return await handler.HandleAsync(command, cancellationToken);
        }
    }
}
