namespace Shared.Application.Queries
{
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public int TotalPages {get; private set;}
        public int PageNumber { get; private set; } = 1;
        public int PageSize {get; private set;}
        public int TotalCount {get; private set;}
        public int ItemsFrom {get; private set;}
        public int ItemsTo {get; private set;}
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageSize = pageSize;
            PageNumber = pageNumber;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
