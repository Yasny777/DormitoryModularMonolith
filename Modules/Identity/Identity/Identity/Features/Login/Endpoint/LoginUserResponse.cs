﻿namespace Identity.Identity.Features.Login.Endpoint;

public record LoginUserResponse(string Token, DateTime TokenExpiryTime);