using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.UpdateDormitory.Handler;

public record UpdateDormitoryCommand(
    Guid DormitoryId,
    string Name,
    string Category,
    AddressDto Address) : ICommand<UpdateDormitoryResult>;