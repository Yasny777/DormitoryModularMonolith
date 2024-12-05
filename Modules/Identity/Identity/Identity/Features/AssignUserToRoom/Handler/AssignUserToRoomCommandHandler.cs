using Identity.Identity.EventHandlers;
using Shared.Contracts.CQRS;
using Shared.Exceptions;

namespace Identity.Identity.Features.AssignUserToRoom.Handler;

internal class AssignUserToRoomCommandHandler(UserManager<AppUser> userManager) : ICommandHandler<AssignUserToRoomCommand, AssignUserToRoomResult>
{
    public async Task<AssignUserToRoomResult> Handle(AssignUserToRoomCommand command, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(command.UserId.ToString());

        if (user == null) throw new NotFoundException("User with id not found");

        user.ReservationId = command.ReservationId;
        user.RoomId = command.RoomId;

        await userManager.UpdateAsync(user);
        return new AssignUserToRoomResult(true);
    }
}