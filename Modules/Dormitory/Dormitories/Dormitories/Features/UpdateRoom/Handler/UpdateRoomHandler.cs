using Dormitories.Contracts.Dto;
using Dormitories.Data.Repository;
using Dormitories.Dormitories.Features.UpdateRoom.Endpoint;
using FluentValidation;
using Shared.Contracts.CQRS;
using Shared.Exceptions;

namespace Dormitories.Dormitories.Features.UpdateRoom.Handler;

public class UpdateRoomHandler(IDormitoryRepository repository)
    : ICommandHandler<UpdateRoomCommand, UpdateRoomResult>
{
    public async Task<UpdateRoomResult> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        var dormitory = await repository.GetDormitoryById(command.DormitoryId, cancellationToken);

        if (dormitory == null)
            throw new NotFoundException($"Dormitory with ID {command.DormitoryId} not found.");

        var room = dormitory.UpdateRoom(command.RoomId, command.Number, command.Capacity, command.Price);

        await repository.SaveChangesAsync(cancellationToken);

        var roomDto = room.Adapt<RoomDto>();
        return new UpdateRoomResult(roomDto);
    }
}

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(x => x.DormitoryId)
            .NotEmpty()
            .WithMessage("Dormitory ID cannot be empty.");

        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage("Room ID cannot be empty.");

        RuleFor(x => x.Number)
            .MaximumLength(10)
            .WithMessage("Room number cannot exceed 10 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Number));

        RuleFor(x => x.Capacity)
            .GreaterThan(0)
            .WithMessage("Capacity must be greater than 0.")
            .When(x => x.Capacity.HasValue);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a positive value.")
            .When(x => x.Price.HasValue);
    }
}