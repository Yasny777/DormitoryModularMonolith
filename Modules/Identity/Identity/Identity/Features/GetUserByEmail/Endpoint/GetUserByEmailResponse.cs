namespace Identity.Identity.Features.GetUserByEmail.Endpoint;

public record GetUserByEmailResponse(Guid Id, string UserName, string Email, IList<string> Roles);