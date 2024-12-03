using Reservations.Reservations.Dto;
using Reservations.Reservations.Features.CreateSemester.Endpoint;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CreateSemester.Handler;

public record CreateSemesterCommand(SemesterDto SemesterDto) : ICommand<CreateSemesterResult>;