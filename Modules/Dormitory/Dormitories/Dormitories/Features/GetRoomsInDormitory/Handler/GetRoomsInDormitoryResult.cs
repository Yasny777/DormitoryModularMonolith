using Dormitories.Contracts.Dto;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Handler;

internal class GetRoomsInDormitoryResult : PagedResult<RoomDto>
{
    public GetRoomsInDormitoryResult(
        List<RoomDto> items,
        long totalCount,
        int pageSize,
        int pageNumber
    ) : base(items, totalCount, pageSize, pageNumber)
    {
        
    }
}