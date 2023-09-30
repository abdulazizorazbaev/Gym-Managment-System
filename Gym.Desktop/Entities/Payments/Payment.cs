using Gym.Desktop.Enums;
using System;

namespace Gym.Desktop.Entities.Payments;

public sealed class Payment : Auditable
{
    public long ClientId { get; set; }

    public string PaymentType { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;
}
