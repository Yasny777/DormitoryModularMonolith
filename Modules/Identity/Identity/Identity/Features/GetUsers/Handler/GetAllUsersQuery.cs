using Shared.Contracts.CQRS;

namespace Identity.Identity.Features.GetUsers.Handler;

internal record GetAllUsersQuery() : IQuery<GetAllUsersResult>;