namespace PB.BaseResult.Communication
{
    public class PagedResult<T> : Result<IEnumerable<T>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasData { get; set; }

        public PagedResult(IEnumerable<T> data,int totalPages, int pageNumber, int pageSize) : base(data, null)
        {
            this.PageNumber = pageNumber;
            this.TotalPages = totalPages;
            this.PageSize = pageSize;
            this.HasData = data.Any();
        }
    }
}