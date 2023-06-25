using System;

namespace Gym.Desktop.Entities.Instructors;

public sealed class Instructor : Human
{
    public long PackageId { get; set; }

    public float Salary { get; set; }

    public string Title { get; set; } = String.Empty;
}