namespace Dormitories.Dormitories.Dto;

public record RoomDto(Guid Id, Guid DormitoryId, string Number,
    string Category, int Capacity, decimal Price);