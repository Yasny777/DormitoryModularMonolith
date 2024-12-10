namespace Identity.Identity.Features.GetUserByEmail.Handler;

internal record GetUserByEmailQuery(string Email) : IQuery<GetUserByEmailResult>;