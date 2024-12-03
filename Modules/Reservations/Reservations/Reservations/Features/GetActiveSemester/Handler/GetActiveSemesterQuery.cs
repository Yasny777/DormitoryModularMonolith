using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.GetActiveSemester.Handler;

public record GetActiveSemesterQuery() : IQuery<GetActiveSemesterResult>;