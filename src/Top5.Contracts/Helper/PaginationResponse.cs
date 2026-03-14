namespace Top5.Contracts.Helper
{
    public class PaginationResponse<T>
    {
            public IEnumerable<T> Data { get; set; } = new List<T>();

            public int PageNumber { get; set; }

            public int PageSize { get; set; }

            public int TotalCount { get; set; }

            public int TotalPages { get; set; }

            public bool HasNextPage => PageNumber < TotalPages;

            public bool HasPreviousPage => PageNumber > 1;
        
    }
}
