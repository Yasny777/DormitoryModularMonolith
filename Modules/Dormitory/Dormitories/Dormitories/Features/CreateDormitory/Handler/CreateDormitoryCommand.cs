namespace Dormitories.Dormitories.Features.CreateDormitory.Handler;

public record CreateDormitoryCommand(DormitoryDto DormitoryDto) : ICommand<CreateDormitoryResult>;