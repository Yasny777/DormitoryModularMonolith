﻿namespace Reservations.Reservations.Features.GetUserReservationsById.Handler;

internal record GetUserReservationsByIdResult(List<ReservationDto> Reservations);