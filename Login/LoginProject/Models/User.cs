using System;
using System.Collections.Generic;

namespace ClinicConnect.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public string? Title { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    public string? PhoneNumber { get; set; }

    public string? StreetAddress { get; set; }

    public string? Suburb { get; set; }

    public string? State { get; set; }

    public int? Postcode { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Practitioner> Practitioners { get; set; } = new List<Practitioner>();
}
