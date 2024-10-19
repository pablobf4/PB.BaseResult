namespace PB.BaseResult.DTO
{
    public class PaginationFilterDTO
    {
        public PaginationFilterDTO()
        {
            PageSize = 10;
            PageNumber = 1;
        }
        public PaginationFilterDTO(int? pageSize = null, int? pageNumber = null)
        {
            PageSize = pageSize ?? 10;
            PageNumber = pageNumber ?? 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
