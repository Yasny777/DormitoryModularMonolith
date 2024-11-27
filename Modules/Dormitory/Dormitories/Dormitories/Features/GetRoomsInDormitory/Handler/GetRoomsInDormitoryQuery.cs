using Dormitories.Dormitories.Constants;
using Dormitories.Dormitories.Features.GetDormitories.Endpoint;
using Shared.Constants;
using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Handler;

public record GetRoomsInDormitoryQuery(
    Guid DormitoryId,
    int PageNumber,
    int PageSize,
    string SortBy,
    SortDirection SortDirection)
    :  IQuery<GetRoomsInDormitoryResult>;