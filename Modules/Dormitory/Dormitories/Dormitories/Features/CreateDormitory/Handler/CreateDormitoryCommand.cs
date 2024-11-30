using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.CreateDormitory.Handler;

//todo make command and queries internal for better hermetization
public record CreateDormitoryCommand(DormitoryDto DormitoryDto) : ICommand<CreateDormitoryResult>;