using System;

namespace Gym.Desktop.Entities.Sessions;

public class Session : Auditable
{
    public long InstructorId { get; set; }

    public long MembershipId { get; set; }

    public DateTime DestinationDate { get; set; }

    public bool IsAttended { get; set; }

    public string Description { get; set; } = String.Empty;
}
