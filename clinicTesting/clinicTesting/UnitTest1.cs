using mainprogram;
using NUnit.Framework;
using cliniclibrary;
using System.Data.SqlClient;

namespace clinicTesting
{
    [TestFixture]
    public class Tests
    {
        ClinicRepository clinicRepository;
        [SetUp]
        public void Setup()
        {
            clinicRepository = new ClinicRepository();

        }


        // ----------------------- Testing staff logins -----------------------
        [Test]
        public void validateLoginUserNotFound()
        {
            Assert.Throws<Exception>(() => clinicRepository.loginScreen("henry", "dustbun"));

        }
        [Test]
        public void validateLoginWrongpassword()
        {
            
            Assert.Throws<Exception>(() => clinicRepository.loginScreen("dustin20", "12345"));
            //Assert.AreEqual(expres, actualres);
        }

        [Test]

        public void validateLoginSuccess()
        {
            bool actualres = clinicRepository.loginScreen("dustin20", "dustbun@12");
            bool expres = false;
            Assert.AreEqual(expres, actualres);
        }

        // ----------------------- Testing Add new patient -----------------------
        [Test]
        public void addPatientSuccess()
        {
            DateTime dob = DateTime.Parse("10/05/2005");

            Patients userdata = new Patients(3434967687, "harshithah", "subbramanian", "Female", 21, dob);
            Patients expdata = clinicRepository.addPatient(userdata);
            Assert.AreEqual(userdata, expdata);

        }
        [Test]
        public void addPatientFailure()
        {
            DateTime dob = DateTime.Parse("10/05/2001");

            Patients userdata = new Patients(1234345456, "harshithah", "K S", "Female", 21, dob);

            Assert.Throws<SqlException>(() => clinicRepository.addPatient(userdata));

        }

        // ----------------------- Testing string, digits date formats -----------------------
        [Test]
        public void checkIDFalse()
        {
            // does it conatisn 10 digits
            long id = 1344545678;
            bool expres = false;
            bool actualres = clinicRepository.checkIdDigits(id);
            Assert.AreEqual(expres, actualres);

        }
        [Test]
        public void checkIsdigitIDtrue()
        {
            // does it conatisn 10 digits
            long id = 34546;
            bool expres = true;
            bool actualres = clinicRepository.checkIdDigits(id);
            Assert.AreEqual(expres, actualres);

        }
        [Test]
        public void checkIsNametrue()
        {
            string name = "harshithah";
            bool expres = true;
            bool actualres = clinicRepository.checkString(name);
            Assert.AreEqual(expres, actualres);
        }
        public void checkIsNamefalse()
        {
            string name = "#er$";
            bool expres = false;
            bool actualres = clinicRepository.checkString(name);
            Assert.AreEqual(expres, actualres);
        }

        [Test]
        public void checkgender()
        {
            string expgen = "Male";
            string actualgen = clinicRepository.getGender(1);
            Assert.AreEqual(expgen, actualgen);
        }
        [Test]
        public void checkGenderException()
        {
            
            Assert.Throws<Exception>(() => clinicRepository.getGender(90));
        }
        [Test]
        public void checkAppointDateSucess()
        {
            DateTime date = DateTime.Parse("10/11/22");
            bool expres = true;
            bool actualres = clinicRepository.checkdateFormatforAppoint(date);
            Assert.AreEqual(expres, actualres);

        }

        [Test]

        public void checkAppointDateFail()
        {
            DateTime date = DateTime.Parse("10/11/25");
            bool expres = false;
            bool actualres = clinicRepository.checkdateFormatforAppoint(date);
            Assert.AreEqual(expres, actualres);

        }
    }
}