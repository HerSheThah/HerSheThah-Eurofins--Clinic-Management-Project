using System;
using System.Collections.Generic;

namespace entityFrameEg.Models
{
    public partial class DoctorDetail
    {
        public DoctorDetail()
        {
            Appointments = new HashSet<Appointment>();
        }

        public long DoctorId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Sex { get; set; }
        public string? Specialization { get; set; }
        public int SpecializationId { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
