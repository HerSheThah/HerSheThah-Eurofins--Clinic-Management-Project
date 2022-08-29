
using System;
using entityFrameEg.Models;

namespace entityFrameEg
{
    internal class Program
    {
        public static ClinicManagementContext clinicdb = new ClinicManagementContext();
        public static PatientDetail p = new PatientDetail();

        static void Main()
        {
            //addNewpatient();
            //deletePatient();
            update();
            displaypatientDetails();

        }

        private static void addNewpatient()
        {
            using (var clinicdb = new ClinicManagementContext())
            {
                Console.WriteLine("Enter firstname, lastname sex age dob");
                p.PatientId = long.Parse(Console.ReadLine());
                p.Firstname = Console.ReadLine();
                p.Lastname = Console.ReadLine();
                p.Sex = Console.ReadLine();
                p.Age = int.Parse(Console.ReadLine());
                p.Dateofbirth = DateTime.Parse(Console.ReadLine());
                clinicdb.PatientDetails.Add(p);
                clinicdb.SaveChanges();
            }
        }

        private static void update()
        {
            using(var clinicdb = new ClinicManagementContext())
            {
                
                Console.WriteLine("enter id: ");
                long id= long.Parse(Console.ReadLine());
                p = clinicdb.PatientDetails.Find(id);
                Console.WriteLine(p);
                Console.WriteLine("Enter name, age: ");
                p.Firstname= Console.ReadLine();
                p.Age = int.Parse(Console.ReadLine());
                clinicdb.PatientDetails.Update(p);
                clinicdb.SaveChanges();

            }
        }

        private static void deletePatient()
        {
            using(var clinicdb = new ClinicManagementContext())
            {
                Console.WriteLine("Enter id: ");
                long id = long.Parse(Console.ReadLine());
                p=clinicdb.PatientDetails.Find(id);

                clinicdb.PatientDetails.Remove(p);
                clinicdb.SaveChanges();
            }
        }
        private static void displaypatientDetails()
        {
            foreach (var item in clinicdb.PatientDetails)
            {
                Console.WriteLine(item.PatientId + " " + item.Firstname + " " + item.Lastname + " " + item.Sex + " " + item.Age + " " + item.Dateofbirth);
            }
        }
    }
}

