using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliniclibrary
{
    public class Appointments
    {
        public long appointmentId { get;set;}
        public long patientId { get; set; }

        public string patientname { get; set; }
        public string specialization { get; set; }
        public int specializationID { get; set; }

        public string doctorname { get; set; }
        public DateTime visitdate { get; set; }

        public string appointmentStartTime { get; set; }
        public string appointmentEndTime { get; set; }


        public Appointments() { }
        public Appointments(long appointmentId,long patientId, string patientname, int specializationID, string specialization, string doctorname, DateTime visitdate, string appointmentStartTime, string appointmentEndTime)
        {
            this.appointmentId = appointmentId;
            this.patientId = patientId;
            this.patientname = patientname;
            this.specialization = specialization;
            this.specializationID = specializationID;

            this.doctorname = doctorname;
            this.visitdate = visitdate;
            this.appointmentStartTime = appointmentStartTime;
            this.appointmentEndTime = appointmentEndTime;

        }

    }
}
