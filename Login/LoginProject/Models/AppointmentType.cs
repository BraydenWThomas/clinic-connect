using System;
using System.Collections.Generic;

namespace ClinicConnect.Models;

public partial class AppointmentType
{
    public int AppointmentTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
