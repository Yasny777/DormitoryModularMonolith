using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.GetDormitories.Handler;

public record GetDormitoriesQuery() : IQuery<GetDormitoriesResult>;