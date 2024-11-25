using Identity.Identity.Constants;
using Identity.Identity.Exceptions;
using Identity.Identity.Features.Register.Endpoint;
using Identity.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Shared.CQRS;

namespace Identity.Identity.Features.Register.Handler;
//todo validate password and confirm password
internal class RegisterUserHandler
    (UserManager<AppUser> userManager)
    : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user != null) throw new UserExistsException($"User with email {request.Email} already exists");

        var newUser = AppUser.Create(request.Email);
        var createResult = await userManager.CreateAsync(newUser, request.Password);

        if (!createResult.Succeeded)
        {
            throw new Exception($"Failed to create user {createResult.Errors}");
        }

// Użytkownik został utworzony, teraz przypisz rolę
        var roleResult = await userManager.AddToRoleAsync(newUser, AppRoles.Candidate);

        if (!roleResult.Succeeded)
        {
            throw new Exception($"Failed to add user to role {roleResult.Errors}");
        }


        return new RegisterUserResult(true);
    }
}