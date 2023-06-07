using Shared.Abstractions.Dispatchers;
using Shared.Abstractions.Events;
using Shared.Abstractions.Mediation.Commands;
using Shared.Abstractions.Mediation.Queries;

namespace Shared.Infrastructure.Dispatchers
{
    internal sealed class Dispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public Dispatcher(ICommandDispatcher commandDispatcher,
                          IEventDispatcher eventDispatcher,
                          IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _eventDispatcher = eventDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return result;
        }

        public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            var result = await _commandDispatcher.SendAsync(command, cancellationToken);
            return result;
        }

        //public Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
        //    => _commandDispatcher.SendAsync(command, cancellationToken);

        //public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        //    => _commandDispatcher.SendAsync(command, cancellationToken);

        //public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
        //    => _eventDispatcher.PublishAsync(@event, cancellationToken);

        //public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        //    => _queryDispatcher.QueryAsync(query, cancellationToken);

    }
}
