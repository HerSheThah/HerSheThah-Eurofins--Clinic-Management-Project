using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cliniclibrary;
using clinicDB;
using System.Text.RegularExpressions;

namespace mainprogram
{
    public class ClinicRepository : Iclinicmanagement
    {

        ClinicDatabase clinicdb = new ClinicDatabase();


        // ---------- Checking with db with the user exists and entered correct password ----------
        public bool loginScreen(string username, string password)
        {
            string staffname = clinicdb.getUserPassword(username, password);
            if(staffname != null)
            {
                Console.WriteLine("Welcome "+ staffname+" Successfully logged into your account");
                return false;
            }
            else
            {
                Console.WriteLine("Username not found!");
                return true;

            }

        }

        // ---------- Displaying all user options ----------

        public int homeScreen()
        {
            int choice = 0;
            bool flag = true;
            
            do
            {Console.WriteLine("\nPress '1' to view all the doctors and their available timing\n" +
                "Press '2' to add new patient detail\n" +
                "Press '3' to schedule an appointment\n" +
                "Press '4' to cancel an appointment\n" +
                "Press '5' to logout\n");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice <= 0 || choice > 5)
                        Console.WriteLine("Enter valid choice!");
                    else
                        flag = false;
                }
                catch (FormatException f)
                {
                    Console.WriteLine("Enter only digits!");
                }
            } while (flag);
            return choice;
            
        }


        // ---------- Displaying doctor details ----------

        public List<Doctordetails> displayDoctorDetails()
        {
            
            return clinicdb.getDoctorDetails();
            
        }
        // ---------- Adding new patient ----------
        public Patients addPatient(string fname, string lname, DateTime dob, int age, string gender)
        {
            return clinicdb.addNewPatient(fname, lname, dob, age, gender);

        }

        // ---------- Scheduling new appointment ----------

        public Appointments scheduleAppoinment(long patientid, int specId, DateTime appointdate)
        {

            return clinicdb.scheduleNewAppointment(patientid, specId, appointdate);

        }

        // ---------- cancelling appointment ----------

        public bool cancelAppoinment(long id)
        {
            return clinicdb.cancelAppointment(id);
            
        }

        // ---------- Checking if id has 10 digits ----------

        public bool checkIdDigits(long id)
        {
            if (id > 9999999999 || id < 1000000000)
                return true;
            return false;
        }

        // ---------- Checking if name is in correct format ----------

        public bool checkString(string name)
        {
            if (Regex.Match(name, "^[a-zA-Z]*$").Success)
                return true;
            return false;

        }
    }
}
