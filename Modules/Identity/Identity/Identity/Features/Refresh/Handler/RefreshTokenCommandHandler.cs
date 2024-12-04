using Identity.Identity.Features.Refresh.Endpoint;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using Shared.Exceptions;

namespace Identity.Identity.Features.Refresh.Handler;

internal class RefreshTokenCommandHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService
) : ICommandHandler<RefreshTokenCommand, RefreshTokenResult>
{
    public async Task<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == request.RefreshToken,
            cancellationToken);

        if (user == null || user.RefreshTokenExpiry <= DateTime.UtcNow)
            throw new BadRequestException("Refresh not found or token expired");

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

        return new RefreshTokenResult(tokens.Token, tokens.TokenExpiryTime, tokens.RefreshToken, tokens.ExpiryRefreshTokenTime);
    }
}