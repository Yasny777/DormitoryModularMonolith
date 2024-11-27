using Dormitories.Dormitories.Models;
using Dormitory.Contracts.Dto;

namespace Dormitories.Dormitories.Mapping;

public class RoomMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Room, RoomDto>()
            .Map(dest => dest.IsAvailable, src => src.IsAvailable())
            .Map(dest => dest.TotalOccupants, src => src.TotalOccupants);
    }
}