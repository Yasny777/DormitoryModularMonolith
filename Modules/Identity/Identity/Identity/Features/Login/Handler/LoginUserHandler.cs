using Mapster;
using Shared.Exceptions;

namespace Identity.Identity.Features.Login.Handler;

internal class LoginUserHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService) : ICommandHandler<LoginUserCommand, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null) throw new BadRequestException("Invalid email or password");
        var checkUserPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!checkUserPasswordValid) throw new BadRequestException("Invalid email or password");

        var roles = await userManager.GetRolesAsync(user);
        var userSession = new UserSession(
            Guid.Parse(user.Id),
            user.UserName!,
            user.Email!,
            roles.ToList());

        var tokens = tokenService.GenerateAccessAndRefreshToken(userSession);

        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenExpiry = tokens.ExpiryRefreshTokenTime;

        await userManager.UpdateAsync(user);
        var result = tokens.Adapt<LoginUserResult>();
        return result;
    }


}