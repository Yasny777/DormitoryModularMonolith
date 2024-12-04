using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.CreateDormitory.Handler;

//todo make command and queries internal for better hermetization
internal record CreateDormitoryCommand(DormitoryDto DormitoryDto) : ICommand<CreateDormitoryResult>;