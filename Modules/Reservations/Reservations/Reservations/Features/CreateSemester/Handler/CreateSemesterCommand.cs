using Reservations.Reservations.Dto;
using Reservations.Reservations.Features.CreateSemester.Endpoint;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CreateSemester.Handler;

internal record CreateSemesterCommand(SemesterDto SemesterDto) : ICommand<CreateSemesterResult>;