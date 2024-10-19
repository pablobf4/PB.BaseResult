namespace PB.BaseResult.Communication
{
    public class PagedResult<T>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public bool HasData => Items.Any();
        public IEnumerable<T> Items { get; }

        public PagedResult(IEnumerable<T> items, int totalPages, int pageNumber, int pageSize)
        {
            Items = items;
            TotalPages = totalPages;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}