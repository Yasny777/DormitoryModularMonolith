using Shared.Contracts.CQRS;

namespace Identity.Identity.Features.GetUsers.Handler;

public record GetAllUsersQuery() : IQuery<GetAllUsersResult>;