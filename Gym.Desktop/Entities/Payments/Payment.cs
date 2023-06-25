using Gym.Desktop.Enums;
using System;

namespace Gym.Desktop.Entities.Payments;

public sealed class Payment : Auditable
{
    public long ClientId { get; set; }

    public PaymentType PaymentType { get; set; }

    public string Description { get; set; } = String.Empty;
}
