namespace Dormitories.Dormitories.Features.CreateDormitory.Handler;

internal record CreateDormitoryCommand(DormitoryDto DormitoryDto) : ICommand<CreateDormitoryResult>;