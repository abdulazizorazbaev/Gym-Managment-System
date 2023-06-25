using Gym.Desktop.Enums;
using System;

namespace Gym.Desktop.Entities.Packages;

public class Package : Auditable
{
    public string PackageName { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;

    public string Duration { get; set; } = String.Empty;

    public float Price { get; set; }

    public long Days { get; set; }

    public string Description { get; set; } = String.Empty;
}
