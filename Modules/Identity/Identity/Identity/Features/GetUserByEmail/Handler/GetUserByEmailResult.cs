namespace Identity.Identity.Features.GetUserByEmail.Handler;

internal record GetUserByEmailResult(Guid Id, string UserName, string Email, IList<string> Roles);