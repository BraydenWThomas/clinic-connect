using System;
using System.Collections.Generic;

namespace ClinicConnect.Models;

public partial class Practitioner
{
    public int PractitionerId { get; set; }

    public string? Appointments { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Appointment> AppointmentsNavigation { get; set; } = new List<Appointment>();

    public virtual User? User { get; set; }
}
