﻿using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.GetActiveSemester.Handler;

internal record GetActiveSemesterQuery() : IQuery<GetActiveSemesterResult>;