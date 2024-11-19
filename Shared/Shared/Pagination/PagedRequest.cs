using Shared.Constants;

namespace Shared.Pagination;

public class PagedRequest
{
    public PagedRequest(int pageNumber, int pageSize, string sortBy, SortDirection sortDirection)
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