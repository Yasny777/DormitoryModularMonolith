namespace Dormitories.Dormitories.Dto;

// todo narazie bez jakichs szczegolowych info
public record DormitoryDto(Guid Id, string Name, string Category, string ContactEmail, string ContactNumber, AddressDto Address);