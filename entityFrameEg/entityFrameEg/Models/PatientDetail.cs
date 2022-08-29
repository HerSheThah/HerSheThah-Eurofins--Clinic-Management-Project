using System;
using System.Collections.Generic;

namespace entityFrameEg.Models
{
    public partial class PatientDetail
    {
        public PatientDetail()
        {
            Appointments = new HashSet<Appointment>();
        }

        public long PatientId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Sex { get; set; }
        public int? Age { get; set; }
        public DateTime? Dateofbirth { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
