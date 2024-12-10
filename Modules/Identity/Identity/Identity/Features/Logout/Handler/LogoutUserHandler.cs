namespace Identity.Identity.Features.Logout.Handler;

internal class LogoutUserHandler(UserManager<AppUser> userManager) : ICommandHandler<LogoutUserCommand, LogoutUserResult>
{
    public async Task<LogoutUserResult> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null) throw new NotFoundException("User not found");

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;

        await userManager.UpdateAsync(user);

        return new LogoutUserResult(true);
    }
}