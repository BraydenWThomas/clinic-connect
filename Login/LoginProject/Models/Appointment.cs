using System;
using System.Collections.Generic;

namespace ClinicConnect.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public DateTime Date { get; set; }

    public int? AppointmentTypeId { get; set; }

    public int? PatientId { get; set; }

    public int? PractitionerId { get; set; }

    public virtual AppointmentType? AppointmentType { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Practitioner? Practitioner { get; set; }
}
