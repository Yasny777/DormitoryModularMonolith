using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.UpdateDormitory.Handler;

public record UpdateDormitoryCommand(Guid DormitoryId, string? Name, string? Category, string? ContactEmail, string? ContactNumber) : ICommand<UpdateDormitoryResult>;