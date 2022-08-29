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


         //---------- Checking with db with the user exists and entered correct password ----------
        public bool loginScreen(string username, string password)
        {
            string staffname = clinicdb.getUserPassword(username, password);
            if (staffname != null)
            {
                Console.WriteLine("Welcome " + staffname + " Successfully logged into your account");
                return false;
            }
            else
            {
                throw new Exception("Username not found!");
            }

        }

        // ---------- Validating user login ----------

        public bool getLoginandValidate()
        {
            bool flag = true;
            do
            {
                try
                {
                    Console.WriteLine("\n Enter username and password to login");
                    string username = Console.ReadLine();
                    string password = Console.ReadLine();
                    if (username.Count() > 0 && password.Count() > 0)
                    {

                        if (username.Count() > 10)
                            throw new Exception("Username should not be greater than 10");
                        else
                            flag = loginScreen(username, password);
                    }
                    else
                    {
                        throw new Exception("Username or password cannot be null");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (flag);
            return true;
        }


        // ---------- Displaying all user options ----------

        public int homeScreen()
        {
            int choice = 0;
            bool flag = true;
            
            do
            {Console.WriteLine("\nPress '1' to view all the doctors and their available timings\n" +
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
            List<Doctordetails> docDet = clinicdb.getDoctorDetails();
            if (docDet.Count > 0)
                return clinicdb.getDoctorDetails();
            else
                throw new Exception("No doctor details found");
                
            
        }
       
        // ---------- Adding new patient ----------
        public Patients addPatient(Patients newpatient)
        {
            Patients newpatientdb = clinicdb.addNewPatient(newpatient);
            if (newpatientdb != null)
                return newpatientdb;
            else
                throw new Exception("Error in adding new patient");
        }

        // ---------- Scheduling new appointment ----------
        public Appointments scheduleAppoinment(Appointments app)
        {
            Appointments newapp = clinicdb.scheduleNewAppointment(app);
            if (newapp != null)
                return newapp;
            else
                throw new Exception("Error in scheduling new appoinment");

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

        // -------------------------------------------------------------------------------------
        // Getting id of 10 digit
        public long getId()
        {
            Random rand = new Random();
            Int64 transId = rand.NextInt64(999999999, 10000000000);
            return transId;
        }

        // Getting age from dob
        public int getage(DateTime dob)
        {
            return DateTime.Today.Year - dob.Year;
        }

        // getting gender 
        public string getGender(int genderid)
        {
            string gender = "";

            if (genderid == 0)
                gender = "Others";
            else if (genderid == 1)
                gender = "Male";
            else if (genderid == 2)
                gender = "Female";
            else
                throw new Exception("Invalid gender choice");
            return gender;
        }

        // Getting all specs ids

        public List<int> allSpecsid()
        {
            List<int> allspecs = new List<int>();
            Dictionary<int, string> allcollection = clinicdb.getSpecialization();
            if(allcollection.Count != 0)
            {
                foreach (KeyValuePair<int, string> spec in allcollection)
                {

                    allspecs.Add(spec.Key);
                }
            }
            else
                throw new Exception("No specializations found");
            return allspecs;
        }

        // displaying specializations
        public void displaySpec()
        {
            Dictionary<int, string> allcollection = clinicdb.getSpecialization();
            if (allcollection.Count == 0)
                throw new Exception("No specializations found");
            else
            {
                foreach (KeyValuePair<int, string> spec in allcollection)
                {
                    Console.WriteLine("{0}: {1}",
                                spec.Key, spec.Value);
                }
            }
           
        }

        public bool checkdateFormatforAppoint(DateTime date)
        {
            DateTime now = DateTime.Today;
            DateTime checkDate = DateTime.Now.AddYears(+1);
            if (date > now && date <= checkDate)
                return true;
            return false;
        }
    }
}
