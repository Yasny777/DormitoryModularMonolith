using Shared.Constants;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Endpoint;

public class GetRoomsInDormitoryRequest : PagedRequest
{
    public GetRoomsInDormitoryRequest(Guid dormitoryId, int pageNumber = 1,
        int pageSize = 10,
        string sortBy = "number",
        SortDirection sortDirection = SortDirection.Asc) :
        base(pageNumber, pageSize, sortBy, sortDirection)
    {
        DormitoryId = dormitoryId;
    }

    public Guid DormitoryId { get; set; }
}

