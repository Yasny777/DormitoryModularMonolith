using Identity.Identity.Dto;

namespace Identity.Identity.Features.GetUsers.Handler;

internal record GetAllUsersResult(List<UserDto> Users);