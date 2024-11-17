using Dormitories.Dormitories.Dto;
using Shared.Features.Endpoints;

namespace Dormitories.Dormitories.Features.GetDormitories.Endpoint;

public record GetDormitoriesResponse(List<DormitoryDto> Dormitories);