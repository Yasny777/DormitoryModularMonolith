using Dormitories.Data.Repository;
using Dormitories.Dormitories.Features.UpdateDormitory.Endpoint;
using Shared.Contracts.CQRS;
using Shared.Exceptions;

namespace Dormitories.Dormitories.Features.UpdateDormitory.Handler;

public class UpdateDormitoryHandler(IDormitoryRepository repository)
    : ICommandHandler<UpdateDormitoryCommand, UpdateDormitoryResult>
{
    public async Task<UpdateDormitoryResult> Handle(UpdateDormitoryCommand command, CancellationToken cancellationToken)
    {
        var dormitory = await repository.GetDormitoryById(command.DormitoryId, cancellationToken);

        if (dormitory == null)
            throw new NotFoundException($"Dormitory with ID {command.DormitoryId} not found.");

        dormitory.Update(command.Name, command.Category, command.ContactEmail, command.ContactNumber);

        await repository.SaveChangesAsync(cancellationToken);
        var dormitoryDto = dormitory.Adapt<DormitoryDto>();
        return new UpdateDormitoryResult(dormitoryDto);
    }
}