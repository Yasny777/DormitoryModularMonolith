using Dormitories.Dormitories.Dto;

namespace Dormitories.Dormitories.Features.GetDormitories.Handler;

public record GetDormitoriesResult(List<DormitoryDto> Dormitories);