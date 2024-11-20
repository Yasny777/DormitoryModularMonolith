using Dormitories.Data.Repository;

namespace Dormitories.Dormitories.Features.GetDormitories.Handler;

internal class GetDormitoriesHandler(IDormitoryRepository repository) : IQueryHandler<GetDormitoriesQuery, GetDormitoriesResult>
{
    public async Task<GetDormitoriesResult> Handle(GetDormitoriesQuery query, CancellationToken cancellationToken)
    {
        var dormitories = await repository.GetDormitories(true, cancellationToken);

        var dormitoriesDto = dormitories.Adapt<List<DormitoryDto>>();

        return new GetDormitoriesResult(dormitoriesDto);
    }
}