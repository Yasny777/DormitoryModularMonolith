using Shared.DDD;

namespace Identity.Identity.Models;

public class AppUser : IdentityUser, IEntity
{

    // IEntity implementation for Interceptors
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    // extended AppUser props
    public Guid? ReservationId { get; set; }

    public Guid? RoomId { get; set; }
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiry { get; set; }

}