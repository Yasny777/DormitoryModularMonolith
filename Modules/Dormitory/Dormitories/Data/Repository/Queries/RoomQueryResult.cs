﻿namespace Dormitories.Data.Repository.Queries;

public record RoomQueryResult(List<Room> Rooms, long TotalCount)
{
}