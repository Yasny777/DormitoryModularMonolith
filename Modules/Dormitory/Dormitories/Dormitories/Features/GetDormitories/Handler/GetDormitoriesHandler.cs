using Dormitories.Data.Repository;
using Dormitories.Dormitories.Dto;
using Mapster;

namespace Dormitories.Dormitories.Features.GetDormitories.Handler;

public class GetDormitoriesHandler(IDormitoryRepository repository) : IQueryHandler<GetDormitoriesQuery, GetDormitoriesResult>
{
    public async Task<GetDormitoriesResult> Handle(GetDormitoriesQuery request, CancellationToken cancellationToken)
    {
        var dormitories = await repository.GetDormitories(true, cancellationToken);

        var dormitoriesDto = dormitories.Adapt<List<DormitoryDto>>();

        return new GetDormitoriesResult(dormitoriesDto);
    }
}