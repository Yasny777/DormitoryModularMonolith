using Dormitories.Contracts.Dto;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Endpoint;

public record GetRoomsInDormitoryResponse(
    List<RoomDto> Items,
    int TotalPages,
    int ItemsFrom,
    int ItemsTo,
    long TotalItemsCount);