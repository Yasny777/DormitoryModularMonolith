namespace Dormitories.Dormitories.Dto;

public record DormitoryDto(Guid Id, string Name,
    string Category, string ContactEmail, string ContactNumber, AddressDto Address);