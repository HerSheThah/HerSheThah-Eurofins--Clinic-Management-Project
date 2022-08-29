using System;
using System.Data.SqlClient;
using cliniclibrary;
using clinicDB;
using System.Globalization;
using System.Text.RegularExpressions;


namespace mainprogram
{
    public class Program
    {
        static void Main()
        {
            // To check if user is logged in
            bool isLoggedin = false;
            ClinicRepository clinic = new ClinicRepository();
            ClinicDatabase clidb = new ClinicDatabase();

            Console.WriteLine("*****************************************************************************");
            Console.WriteLine("*************************** Welcome to xyz Clinic ***************************");
            Console.WriteLine("*****************************************************************************");

            // loginScreen screen
            isLoggedin = clinic.getLoginandValidate();
            bool toContinue = true;
            while (isLoggedin)
            {
                // Displaying all the options 
                int choice = clinic.homeScreen();
                switch (choice)
                {
                    case 1:
                        {
                            // To view all doctors list
                            Console.WriteLine("*****************************************************************************");
                            List<Doctordetails> doctordetilas = clinic.displayDoctorDetails();
                            foreach (var item in doctordetilas)
                            {
                                Console.WriteLine(
                                    "Id: " + item.id +
                                    "\tName: " + item.fname + " " + item.lname +
                                    "\tSex: " + item.sex +
                                    "\tSpecialization: " + item.specialization +
                                    "\tSpecializationid: " + item.specializationid +
                                    "\tStarttime: " + item.starttime +
                                    "\tEndtime: " + item.endtime + "\n\n");
                                Console.WriteLine("**********");
                            }
                        }
                        break;
                    case 2:
                        {
                            // To add new patient
                            Console.WriteLine("Enter Firstname and lastname");
                            bool flag = true;
                            do
                            {
                                string firstName = Console.ReadLine();
                                string lastName = Console.ReadLine();

                                if (clinic.checkString(firstName) && clinic.checkString(lastName))
                                {
                                    do
                                    {
                                        try
                                        {
                                            Console.WriteLine("Enter date of Birth in given format(10/05/2001)");
                                            DateTime dob = DateTime.Parse(Console.ReadLine());
                                            if (dob <= DateTime.Today)
                                            {
                                                int age = clinic.getage(dob);
                                                do
                                                {
                                                   
                                                        Console.WriteLine("Gender: press '1' for male '2' for Female and '0' for others");
                                                        int genderid = int.Parse(Console.ReadLine());
                                                        
                                                        string gender = clinic.getGender(genderid);
                                                        if(gender!=null)
                                                        {
                                                            flag = false;
                                                            Patients newpatient = clinic.addPatient(new Patients(clinic.getId(), firstName, lastName, gender, age, dob));

                                                            if (newpatient != null)
                                                            {
                                                                Console.WriteLine("***** New patient added *****");
                                                                Console.WriteLine("Patient ID: " + newpatient.id +
                                                                    "\nPatient Name: " + newpatient.fname + " " + newpatient.lname +
                                                                    "\nSex: " + newpatient.sex +
                                                                    "\nAge: " + newpatient.age +
                                                                    "\nDate of Birth: " + newpatient.dob);
                                                            }
                                                        }
                                                } while (flag);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Enter a valid date which is less than today");
                                            }
                                        }
                                        catch (FormatException e)
                                        {
                                            Console.WriteLine("Only date input is accepted");
                                        }

                                    } while (flag);
                                }
                                else
                                {
                                    Console.WriteLine("Enter valid name");
                                }
                            } while (flag);


                        }
                        break;
                    case 3:
                        {
                            // Fix new appointment 
                            Console.WriteLine("*********************************** New Appointment ***********************************");
                            bool flag = true;
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Enter patient id: ");
                                    long id = long.Parse(Console.ReadLine());
                                    if (clinic.checkIdDigits(id))
                                        Console.WriteLine("Id must contain 10 digits");
                                    else
                                    {
                                        if (clidb.checkPatientIdExists(id)!=null)
                                        {
                                            Console.WriteLine("*** Patient Found! ***");
                                            clinic.displaySpec();
                                            List<int> allspecs = clinic.allSpecsid();
                                            do
                                            {
                                                Console.WriteLine("\nChoose Your consultant by using appropriate spetialization id");
                                                    int userSpec = int.Parse(Console.ReadLine());
                                                    if (allspecs.Contains(userSpec))
                                                    {
                                                        do
                                                        {
                                                                Console.WriteLine("Enter the date the appointment has to be fixed (ex: 10/05/22): ");
                                                                DateTime appointDate = DateTime.Parse(Console.ReadLine());
                                                                
                                                                if (clinic.checkdateFormatforAppoint(appointDate))
                                                                {
                                                                    // check if there already an appoitnment fixed in that date 
                                                                    if (clidb.checkAppointmentdates(appointDate, userSpec))
                                                                        Console.WriteLine("***** There is already an appointment fixed in that date.. choose some other dates *****");
                                                                    else
                                                                    {

                                                                        // get patient name with id, get doc details
                                                                        string patientname = clidb.getPatientName(id);
                                                                        Doctordetails docneccessarydetail = clidb.getDocdetailsforAppointment(userSpec);
                                                                        if (patientname != null && docneccessarydetail != null)
                                                                        {

                                                                            Console.WriteLine("\n***** Appointment on that date can be fixed *****\n");
                                                                            Appointments app = clinic.scheduleAppoinment(new Appointments(clinic.getId(), id, patientname, userSpec, docneccessarydetail.specialization, docneccessarydetail.fname, appointDate, docneccessarydetail.starttime, docneccessarydetail.endtime)); 
                                                                            Console.WriteLine("\n Appointment Id: " + app.appointmentId +
                                                                                    "\nPatient Name: " + app.patientname +
                                                                                    "\nSpecialization: " + app.specialization +
                                                                                    "\nDoctor Name: " + app.doctorname +
                                                                                    "\nVisit Date: " + app.visitdate +
                                                                                    "\nAppointment Start time: " + app.appointmentStartTime +
                                                                                "\nAppointment End Time: " + app.appointmentEndTime);
                                                                                flag = false;
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Couldn't get patient doctor details");
                                                                        }
                                                                      
                                                                    }
                                                                }
                                                                   
                                                                else
                                                                    Console.WriteLine("Enter a valid date within a year and not less than today ");
                                                        } while (flag);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("***** Enter Valid choice!!!! *****");
                                                    }
                                                

                                            } while (flag);
                                        }
                                        else
                                            Console.WriteLine("Patient Id doesnot exists!");
                                    }
                                }
                                catch (FormatException f)
                                {
                                    Console.WriteLine("Enter Input in correct format");
                                }
                            } while (flag);
                        }
                        break;
                    case 4:
                        {
                            // Cancel Appointment
                            Console.WriteLine("*****************************************************************************");
                            bool flag = true;
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Enter appointment Id: ");
                                    long appid = long.Parse(Console.ReadLine());
                                    if (clinic.checkIdDigits(appid))
                                        Console.WriteLine("Id must contain 10 digits");
                                    else
                                    {
                                        if (clinic.cancelAppoinment(appid))
                                        {
                                            Console.WriteLine("***** Appointment Successfully Cancelled!! *****");
                                            flag = false;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Appointment Id not found");
                                        }
                                    }
                                }
                                catch (FormatException f)
                                {
                                    Console.WriteLine(f.Message);
                                }
                            } while (flag);
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("******* Logged out from account *******");
                            isLoggedin = clinic.getLoginandValidate();
                            continue;
                        }
                        break;
                    default:
                        Console.WriteLine("invalid choice");
                        break;
                }
                try
                {
                    Console.WriteLine("\nPress any key from '1-9' to continue with banking, press '0' to quit");
                    int cont = int.Parse(Console.ReadLine());
                    if (cont == 0)
                        break;
                    else
                        continue;

                }
                catch (Exception e)
                {
                    continue;
                }
                
        } 
        }
    }
}

