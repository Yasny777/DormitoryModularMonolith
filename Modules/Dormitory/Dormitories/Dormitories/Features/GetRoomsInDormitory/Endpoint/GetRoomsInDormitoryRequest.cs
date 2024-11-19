using Shared.Constants;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Endpoint;

public class GetRoomsInDormitoryRequest : PagedRequest
{
    public GetRoomsInDormitoryRequest(Guid dormitoryId, int pageNumber, int pageSize, string sortBy, SortDirection sortDirection) :
        base(pageNumber, pageSize, sortBy, sortDirection)
    {
        DormitoryId = dormitoryId;
    }

    public Guid DormitoryId { get; set; }
}

