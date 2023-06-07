namespace Shared.Abstractions.Mediation.Queries
{
    public interface IQueryDispatcher
    {
        //Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
        //    where TQuery : IQuery<TResult>;

        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}
