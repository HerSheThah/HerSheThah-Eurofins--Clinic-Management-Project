using System;
using System.Collections.Generic;

namespace entityFrameEg.Models
{
    public partial class Appointment
    {
        public long Appointmentid { get; set; }
        public long? PatientId { get; set; }
        public int? SpecializationId { get; set; }
        public string? Doctor { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? AppointmentTimeFrom { get; set; }
        public string? AppointmentTimeTo { get; set; }

        public virtual PatientDetail? Patient { get; set; }
        public virtual DoctorDetail? Specialization { get; set; }
    }
}
