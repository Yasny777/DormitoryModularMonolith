namespace Reservations.Reservations.Features.CreateSemester.Handler;

internal class CreateSemesterHandler(ReservationDbContext dbContext)
    : ICommandHandler<CreateSemesterCommand, CreateSemesterResult>
{
    public async Task<CreateSemesterResult> Handle(CreateSemesterCommand command, CancellationToken cancellationToken)
    {
        var semesterActive =
            await dbContext.Semesters.SingleOrDefaultAsync(s => s.IsActive == true,
                cancellationToken: cancellationToken);

        if (semesterActive is not null && command.SemesterDto.IsActive)
            throw new BadRequestException("Active semester already exists");
        var semester = CreateNewSemester(command.SemesterDto);

        semester.SetPriorityWindow(command.SemesterDto.PriorityWindow.RoleNames,
            command.SemesterDto.PriorityWindow.StartDateTime,
            command.SemesterDto.PriorityWindow.EndDateTime);

        var result = await dbContext.Semesters.AddAsync(semester, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateSemesterResult(semester.Id);
    }

    private static Semester CreateNewSemester(SemesterDto semesterDto)
    {
        return Semester.Create(
            Guid.NewGuid(),
            semesterDto.Name,
            semesterDto.Number,
            semesterDto.StartDate,
            semesterDto.EndDate,
            semesterDto.IsActive
        );
    }
}