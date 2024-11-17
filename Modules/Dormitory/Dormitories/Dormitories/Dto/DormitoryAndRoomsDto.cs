namespace Dormitories.Dormitories.Dto;

public record DormitoryAndRoomsDto(Guid Id, string Name, string Category, List<RoomDto> Rooms);