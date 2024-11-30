using Dormitories.Dormitories.Constants;
using Shared.Constants;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Endpoint;

public class GetRoomsInDormitoryRequest : PagedRequest
{
    public GetRoomsInDormitoryRequest(
        int pageNumber = 1,
        int pageSize = 10,
        string sortBy = RoomsInDormitorySortBy.Number,
        SortDirection sortDirection = SortDirection.Asc) :
        base(pageNumber, pageSize, sortBy, sortDirection)
    {
    }
}

