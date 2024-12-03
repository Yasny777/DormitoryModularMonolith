using Identity.Identity.Dto;

namespace Identity.Identity.Features.GetUsers.Handler;

public record GetAllUsersResult(List<UserDto> Users);