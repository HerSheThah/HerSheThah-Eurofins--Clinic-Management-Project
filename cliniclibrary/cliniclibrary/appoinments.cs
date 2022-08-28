using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliniclibrary
{
    public class Appointments
    {
        public long appointmentid {get;set;}
        public string patientname { get; set; }
        public string specialization { get; set; }
        public string doctorname { get; set; }
        public DateTime visitdate { get; set; }

        public string appointmentStartTime { get; set; }
        public string appointmentEndTime { get; set; }


        public Appointments() { }
        public Appointments(long appointmentId, string patientname, string specialization, string doctorname, DateTime visitdate, string appointmentStartTime, string appointmentEndTime)
        {
            this.appointmentid = appointmentid;
            this.patientname = patientname;
            this.specialization = specialization;
            this.doctorname = doctorname;
            this.visitdate = visitdate;
            this.appointmentStartTime = appointmentStartTime;
            this.appointmentEndTime = appointmentEndTime;

        }

    }
}
