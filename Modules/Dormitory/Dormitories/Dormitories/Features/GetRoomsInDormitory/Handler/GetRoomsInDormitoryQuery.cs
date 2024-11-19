using Dormitories.Dormitories.Constants;
using Dormitories.Dormitories.Features.GetDormitories.Endpoint;
using Shared.Constants;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Handler;

public record GetRoomsInDormitoryQuery(
    Guid DormitoryId,
    int PageNumber = 1,
    int PageSize = 10,
    string SortBy = RoomsInDormitorySortBy.Number,
    SortDirection SortDirection = SortDirection.Asc)
    :  IQuery<GetRoomsInDormitoryResult>;