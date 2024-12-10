namespace Dormitories.Dormitories.Exceptions;

public class RoomNotFoundException : NotFoundException
{
    public RoomNotFoundException(Guid id) : base("Room", id)
    {
    }
}