namespace Dormitories.Dormitories.Features.UpdateDormitory.Endpoint;

public record UpdateDormitoryRequest(string? Name, string? Category, string? ContactEmail, string? ContactNumber);