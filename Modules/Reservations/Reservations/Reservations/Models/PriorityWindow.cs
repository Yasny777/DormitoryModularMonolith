namespace Reservations.Reservations.Models;

public class PriorityWindow : Entity<Guid>
{
    public Guid SemesterId { get; private set; }
    public Semester Semester { get; private set; } = default!;
    public List<string> RoleNames { get; private set; } = [];
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }

    private PriorityWindow()
    {
    }

    public PriorityWindow(Guid semesterId, Semester semester, List<string> roleNames, DateTime startDateTime, DateTime endDateTime)
    {
        if (startDateTime >= endDateTime)
            throw new ArgumentException("Start date and time must be earlier than end date and time.");

        SemesterId = semesterId;
        Semester = semester;
        RoleNames = roleNames ?? throw new ArgumentNullException(nameof(roleNames));
        StartDateTime = startDateTime.ToUniversalTime();
        EndDateTime = endDateTime.ToUniversalTime();
    }

    public bool IsWithinWindow(DateTime dateTime)
    {
        return dateTime >= StartDateTime && dateTime <= EndDateTime;
    }

    public bool IsRoleAllowed(string role)
    {
        return RoleNames.Contains(role);
    }
}