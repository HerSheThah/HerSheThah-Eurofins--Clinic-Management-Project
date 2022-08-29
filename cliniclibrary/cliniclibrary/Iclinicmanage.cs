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
        public Patients addPatient(Patients newpatient);
        public Appointments scheduleAppoinment(Appointments app);
        public bool cancelAppoinment(long id);
    }
}
