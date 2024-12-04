using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.GetDormitories.Handler;

internal record GetDormitoriesQuery() : IQuery<GetDormitoriesResult>;