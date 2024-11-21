using Shared.Exceptions;

namespace Dormitories.Dormitories.Exceptions;

public class DormitoryNotFoundException(Guid id) : NotFoundException("Dormitory", id)
{
}