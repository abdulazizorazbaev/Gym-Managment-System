using Gym.Desktop.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gym.Desktop.Entities;

public class Human : Auditable
{
    [MaxLength(30)]
    public string FirstName { get; set; } = String.Empty;

    [MaxLength(30)]
    public string LastName { get; set; } = String.Empty;

    public DateOnly Date_of_birth { get; set; }

    public bool IsMale { get; set; }

    [MaxLength(50)]
    public string Email { get; set; } = String.Empty;

    [MaxLength(13)]
    public string PhoneNumber { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;
}
