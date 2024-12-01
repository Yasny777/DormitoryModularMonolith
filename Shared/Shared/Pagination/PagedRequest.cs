using Shared.Constants;

namespace Shared.Pagination;

public class PagedRequest
{
    public PagedRequest(int pageNumber = 1,
        int pageSize = 10,
        string sortBy = "Id",
        SortDirection sortDirection = SortDirection.Asc)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SortBy = sortBy;
        SortDirection = sortDirection;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}