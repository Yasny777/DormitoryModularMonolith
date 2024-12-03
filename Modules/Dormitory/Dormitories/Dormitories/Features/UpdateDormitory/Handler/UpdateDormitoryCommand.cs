using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.UpdateDormitory.Endpoint;

public record UpdateDormitoryCommand(Guid DormitoryId, string? Name, string? Category, string? ContactEmail, string? ContactNumber) : ICommand<UpdateDormitoryResult>;