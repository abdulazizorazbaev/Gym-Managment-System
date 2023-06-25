using System;

namespace Gym.Desktop.Entities.Admins;

public sealed class Admin : Human
{
    public string Password { get; set; } = String.Empty;
}
