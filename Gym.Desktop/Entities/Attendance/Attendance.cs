using System;

namespace Gym.Desktop.Entities.Attendance;

public class Attendance : Auditable
{
    public long ClientId { get; set; }

    public long SessionId { get; set;}

    public string Description { get; set; } = String.Empty;
}
