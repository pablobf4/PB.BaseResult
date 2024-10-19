namespace PB.BaseResult.Communication
{
    public class PaginatedResult
    {
        public PaginatedResult()
        {
            PageSize = 10;
            PageNumber = 1;
        }
        public PaginatedResult(int? pageSize = null, int? pageNumber = null)
        {
            PageSize = pageSize ?? 10;
            PageNumber = pageNumber ?? 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
