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
            bool getLoginandValidate()
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
                                throw new LoginScreen.UsernameException("Username should not be greater than 10");
                            else
                                flag = clinic.loginScreen(username, password);
                        }
                        else
                        {
                            Console.WriteLine("Username or password cannot be null");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                } while (flag);
                return flag;
            }
            if (getLoginandValidate()) {
                Console.WriteLine("Login failed");
            } else
            {
               isLoggedin = true;
            }

            if (isLoggedin)
            {
                bool toContinue = true;
                while (toContinue)
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
                                        "\nName: " + item.fname + " " + item.lname +
                                        "\nSex: " + item.sex +
                                        "\nSpecialization: " + item.specialization +
                                        "\nSpecializationid: " + item.specializationid +
                                        "\nStarttime: " + item.starttime +
                                        "\nEndtime: " + item.endtime + "\n\n");
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
                                                    int age = DateTime.Today.Year - dob.Year;
                                                    do
                                                    {
                                                        try
                                                        {
                                                            Console.WriteLine("Gender: press '1' for male '2' for Female and '0' for others");
                                                            int genderid = int.Parse(Console.ReadLine());
                                                            string gender = "";
                                                            if (genderid == 0 || genderid == 1 || genderid == 2)
                                                            {
                                                                if (genderid == 0)
                                                                    gender = "Others";
                                                                else if (genderid == 1)
                                                                    gender = "Male";
                                                                else if (genderid == 2)
                                                                    gender = "Female";
                                                                flag = false;
                                                                Patients newpatient = clinic.addPatient(firstName, lastName, dob, age, gender);
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
                                                            else
                                                                Console.WriteLine("Enter valid gender choice");
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            Console.WriteLine(e.Message);
                                                        }


                                                    } while (flag);

                                                }
                                                else
                                                {
                                                    Console.WriteLine("Enter a valid date");
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
                                            if (clidb.checkPatientIdExists(id))
                                            {
                                                Console.WriteLine("Patient Found!");
                                                List<int> allspecs = new List<int>();
                                                Dictionary<int, string> allcollection = clidb.getSpecialization();
                                                foreach (KeyValuePair<int, string> spec in allcollection)
                                                {

                                                    allspecs.Add(spec.Key);
                                                }
                                                do
                                                {
                                                    Console.WriteLine("\nChoose Your consultant by using appropriate spetialization id");
                                                    foreach (KeyValuePair<int, string> spec in allcollection)
                                                    {
                                                        Console.WriteLine("{0}: {1}",
                                                                  spec.Key, spec.Value);

                                                    }
                                                    try
                                                    {
                                                        int userSpec = int.Parse(Console.ReadLine());
                                                        if (allspecs.Contains(userSpec))
                                                        {
                                                            do
                                                            {
                                                                try
                                                                {
                                                                    Console.WriteLine("Enter the date the appointment has to be fixed (ex: 10/05/22): ");
                                                                    DateTime appointDate = DateTime.Parse(Console.ReadLine());
                                                                    DateTime now = DateTime.Today;
                                                                    if (appointDate > now)
                                                                        // check if there already an appoitnment fixed in that date 
                                                                        if (clidb.checkAppointmentdates(appointDate, userSpec))
                                                                            Console.WriteLine("***** There is already an appointment fixed in that date.. choose some other dates *****");
                                                                        else
                                                                        {
                                                                            Console.WriteLine("\n***** Appointment on that date can be fixed *****\n");
                                                                            Appointments app = clinic.scheduleAppoinment(id, userSpec, appointDate);
                                                                            Console.WriteLine("\n Appointment Id: "+app.appointmentid+ 
                                                                                "\nPatient Name: " + app.patientname+
                                                                                "\nSpecialization: " + app.specialization +
                                                                                "\nDoctor Name: " + app.doctorname +
                                                                                "\nVisit Date: " + app.visitdate +
                                                                                "\nAppointment Start time: " + app.appointmentStartTime +
                                                                            "\nAppointment End Time: " + app.appointmentEndTime);
                                                                            flag = false;
                                                                        }

                                                                    else
                                                                        Console.WriteLine("Enter date greater than today ");

                                                                }
                                                                catch (FormatException e)
                                                                {
                                                                    Console.WriteLine("Enter valid date");
                                                                }
                                                            } while (flag);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("***** Enter Valid choice!!!! *****");
                                                        }
                                                    }
                                                    catch (FormatException f)
                                                    {
                                                        Console.WriteLine("***** Enter choice in number!!!! *****");
                                                    }

                                                } while (flag);
                                            }
                                            else
                                                Console.WriteLine("Patient Id doesnot exists!");
                                        }
                                    }
                                    catch (FormatException f)
                                    {
                                        Console.WriteLine("Enter the id in digits");
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
                                isLoggedin = false;
                            }
                            break;
                        default:
                            Console.WriteLine("invalid choice");
                            break;
                    }
                    try
                    {
                        Console.WriteLine("\nPress any number from '1-9' to continue with banking, press '0' to quit");
                        int cont = int.Parse(Console.ReadLine());
                        if (cont == 0)
                            toContinue = false;
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            } 
        }
    }
}

