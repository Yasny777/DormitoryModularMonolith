﻿using Dormitories.Dormitories.Exceptions;
using Dormitories.Contracts.Dormitories.GetRoomById;

namespace Dormitories.Dormitories.Features.GetRoomById.Handler;

internal class GetRoomByIdHandler(IDormitoryRepository repository)
    : IQueryHandler<GetRoomByIdQuery, GetRoomByIdResult>
{
    public async Task<GetRoomByIdResult> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await repository.GetRoomById(request.RoomId, cancellationToken);
        if (room == null) throw new RoomNotFoundException(request.RoomId);

        var roomDto = room.Adapt<RoomDto>();
        return new GetRoomByIdResult(roomDto);
    }
}