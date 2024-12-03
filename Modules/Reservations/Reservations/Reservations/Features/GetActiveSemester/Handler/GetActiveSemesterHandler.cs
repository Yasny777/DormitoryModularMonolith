using Mapster;
using Reservations.Reservations.Dto;
using Reservations.Reservations.Features.GetActiveSemester.Endpoint;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.GetActiveSemester.Handler;

internal class GetActiveSemesterHandler(ReservationDbContext dbContext)
    : IQueryHandler<GetActiveSemesterQuery, GetActiveSemesterResult>
{
    public async Task<GetActiveSemesterResult> Handle(GetActiveSemesterQuery query, CancellationToken cancellationToken)
    {
        var semester = await dbContext.Semesters
            .AsNoTracking()
            .Where(s => s.IsActive)
            .FirstOrDefaultAsync(cancellationToken);

        if (semester == null)
        {
            throw new NotFoundException("No active semester found.");
        }

        var semesterDto = semester.Adapt<SemesterDto>();
        return new GetActiveSemesterResult(
            semesterDto
        );
    }
}