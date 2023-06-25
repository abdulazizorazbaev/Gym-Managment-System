using Gym.Desktop.Enums;
using System;

namespace Gym.Desktop.Entities.Memberships;

public sealed class Membership : Auditable
{
    public long ClientId { get; set; }

    public long PackageId { get; set; }

    public long InstructorId { get; set; }

    public MembershipStatus MembershipStatus { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public DateTime PaymentDate { get; set; }

    public bool IsPaid { get; set; }

    public string Description { get; set; } = string.Empty;
}
