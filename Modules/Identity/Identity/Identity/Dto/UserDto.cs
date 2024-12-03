namespace Identity.Identity.Dto;

public record UserDto(Guid Id, string UserName, string Email, List<string> Roles)
{
    
}