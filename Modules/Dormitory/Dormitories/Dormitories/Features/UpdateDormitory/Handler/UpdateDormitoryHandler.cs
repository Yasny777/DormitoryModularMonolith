﻿namespace Dormitories.Dormitories.Features.UpdateDormitory.Handler;

internal class UpdateDormitoryHandler(IDormitoryRepository repository)
    : ICommandHandler<UpdateDormitoryCommand, UpdateDormitoryResult>
{
    public async Task<UpdateDormitoryResult> Handle(UpdateDormitoryCommand command, CancellationToken cancellationToken)
    {
        var dormitory = await repository.GetDormitoryById(command.DormitoryId, cancellationToken);

        if (dormitory == null)
            throw new NotFoundException($"Dormitory with ID {command.DormitoryId} not found.");

        dormitory.Update(command.Name, command.Category,
            Address.Of(command.Address.Street, command.Address.City, command.Address.ZipCode));

        await repository.SaveChangesAsync(cancellationToken);
        var dormitoryDto = dormitory.Adapt<DormitoryDto>();
        return new UpdateDormitoryResult(dormitoryDto);
    }
}