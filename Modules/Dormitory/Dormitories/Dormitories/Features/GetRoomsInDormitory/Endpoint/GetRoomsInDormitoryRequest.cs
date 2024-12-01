using Dormitories.Dormitories.Constants;
using Shared.Constants;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Endpoint;

public class GetRoomsInDormitoryRequest(
    int pageNumber,
    int pageSize,
    string sortBy = RoomsInDormitorySortBy.Number,
    SortDirection sortDirection = SortDirection.Asc,
    decimal? priceFrom = null,
    decimal? priceTo = null,
    int? capacity = null,
    string? category = null)
    : PagedRequest(pageNumber, pageSize, sortBy, sortDirection)
{
    public decimal? PriceFrom { get; set; } = priceFrom;
    public decimal? PriceTo { get; set; } = priceTo;
    public int? Capacity { get; set; } = capacity;
    public string? Category { get; set; } = category;
}