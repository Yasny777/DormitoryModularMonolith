namespace Dormitories.Dormitories.Features.UpdateDormitory.Handler;

internal record UpdateDormitoryCommand(
    Guid DormitoryId,
    string Name,
    string Category,
    AddressDto Address) : ICommand<UpdateDormitoryResult>;