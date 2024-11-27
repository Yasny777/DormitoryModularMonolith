using Dormitories.Data.Repository;
using Dormitories.Dormitories.Models;
using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.CreateDormitory.Handler;

internal class CreateDormitoryHandler(IDormitoryRepository repository)
    : ICommandHandler<CreateDormitoryCommand, CreateDormitoryResult>
{
    public async Task<CreateDormitoryResult> Handle(CreateDormitoryCommand command, CancellationToken cancellationToken)
    {
        var dormitory = CreateNewDormitory(command.DormitoryDto);
        var result = await repository.CreateDormitory(dormitory, cancellationToken);
        return new CreateDormitoryResult(result);
    }

    private Dormitory CreateNewDormitory(DormitoryDto dormitoryDto)
    {
        var dormitoryAddress = Address.Of(dormitoryDto.AddressDto.Street, dormitoryDto.AddressDto.City,
            dormitoryDto.AddressDto.ZipCode);

        var newDormitory = Dormitory.Create(
            Guid.NewGuid(),
            dormitoryDto.Name,
            dormitoryDto.Category,
            dormitoryDto.ContactEmail,
            dormitoryDto.ContactNumber,
            dormitoryAddress
        );

        return newDormitory;
    }
}