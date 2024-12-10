namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Handler;

internal record GetRoomsInDormitoryQuery(
    Guid DormitoryId,
    int PageNumber,
    int PageSize,
    string SortBy,
    SortDirection SortDirection,
    decimal? PriceFrom = null,
    decimal? PriceTo = null,
    int? Capacity = null,
    string? Category = null)
    :  IQuery<GetRoomsInDormitoryResult>;