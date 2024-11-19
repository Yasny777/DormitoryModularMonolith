using Shared.Constants;

namespace Shared.Pagination;

public class PagedRequest
{
    public PagedRequest(int pageNumber = 1, // Domyślnie pierwsza strona
        int pageSize = 10,  // Domyślny rozmiar strony
        string sortBy = "Id", // Domyślne pole sortowania
        SortDirection sortDirection = SortDirection.Asc) // Domyślny kierunek sortowania)
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