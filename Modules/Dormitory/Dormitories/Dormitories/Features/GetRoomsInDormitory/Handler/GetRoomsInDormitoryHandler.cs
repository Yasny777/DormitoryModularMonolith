using Dormitories.Data.Repository;
using Dormitories.Dormitories.Models;
using Dormitories.Contracts.Dto;
using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Handler;

internal class GetRoomsInDormitoryHandler(IDormitoryRepository repository)
    : IQueryHandler<GetRoomsInDormitoryQuery, GetRoomsInDormitoryResult>
{
    public async Task<GetRoomsInDormitoryResult> Handle(GetRoomsInDormitoryQuery query, CancellationToken cancellationToken)
    {
        //todo query validator
        var pageNumber = query.PageNumber;
        var pageSize = query.PageSize;
        //var totalCount = await repository.GetTotalRoomCountInDormitory(query.DormitoryId, cancellationToken);
        var roomsQueryResult = await repository.GetRoomsInDormitoryByQuery(query, true, cancellationToken);

        var roomsDto = roomsQueryResult.Rooms.Adapt<List<RoomDto>>();

        return new GetRoomsInDormitoryResult(roomsDto, roomsQueryResult.TotalCount, pageSize, pageNumber);
    }
}