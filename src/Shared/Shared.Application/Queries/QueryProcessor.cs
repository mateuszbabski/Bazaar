using Shared.Abstractions.Queries;

namespace Shared.Application.Queries
{
    public class QueryProcessor<T> : IQueryProcessor<T> where T : class
    {
        public QueryProcessor()
        {            
        }
        public virtual IQueryable<T> SortQuery(IQueryable<T> baseQuery, string sortBy, string sortDirection)
        {
            return baseQuery;
        }

        public List<T> PageQuery(IEnumerable<T> baseQuery, int pageNumber, int pageSize)
        {
            return baseQuery.Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
        }
    }
}
