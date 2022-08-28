using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliniclibrary
{
    public interface Iclinicmanagement
    {

        public bool loginScreen(string username, string password);
        public List<Doctordetails> displayDoctorDetails();
        public Patients addPatient(string fname, string lname, DateTime dob, int age, string gender);
        public Appointments scheduleAppoinment(long id, int spec, DateTime appointDate);
        public bool cancelAppoinment(long id);
    }
}
