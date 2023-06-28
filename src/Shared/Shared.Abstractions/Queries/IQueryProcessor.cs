namespace Shared.Abstractions.Queries
{
    public interface IQueryProcessor
    {

    }
    public interface IQueryProcessor<T> : IQueryProcessor where T : class
    {
        IQueryable<T> SortQuery(IQueryable<T> baseQuery, string sortBy, string sortDirection);
        List<T> PageQuery(IEnumerable<T> baseQuery, int pageNumber, int pageSize);
    }
}
