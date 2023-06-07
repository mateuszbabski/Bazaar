using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Mediation.Queries;

namespace Shared.Infrastructure.Mediation.Queries
{
    internal sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        //public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) 
        //    where TQuery : IQuery<TResult>
        //{
        //    var handler = _serviceProvider.GetRequiredService<IQueryHandler<IQuery<TResult>, TResult>>();
        //    return await handler.HandleAsync(query, cancellationToken);
        //}

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<IQuery<TResult>, TResult>>();
            return await handler.HandleAsync(query, cancellationToken);
        }
    }
}
