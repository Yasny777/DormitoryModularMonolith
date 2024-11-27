using Dormitories.Contracts.Dormitories.GetRoomById;
using MediatR;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CreateReservation.Handler;

internal class CreateReservationHandler(ISender sender)
    : ICommandHandler<CreateReservationCommand, CreateReservationResult>
{
    public async Task<CreateReservationResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var room = await sender.Send(new GetRoomByIdQuery(request.RoomId));

        return new CreateReservationResult();


    }
}