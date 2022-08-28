using mainprogram;
using NUnit.Framework;
using cliniclibrary;

namespace clinicTesting
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        // ----------------------- Testing staff logins -----------------------
        [Test]
        public void validateLoginUserNotFound()
        {
            ClinicRepository clinicRepository = new ClinicRepository();
            bool actualres = clinicRepository.loginScreen("henry", "dustbun@12");
            bool expres = true;
            Assert.AreEqual(expres, actualres);
        }
        [Test]
        public void validateLoginWrongpassword()
        {
        LoginScreen loginScreen = new LoginScreen();
            ClinicRepository clinicRepository = new ClinicRepository();
            Assert.Throws<LoginScreen.PasswordException>(() => clinicRepository.loginScreen("dustin20", "12345"));
            //Assert.AreEqual(expres, actualres);
        }
 
        [Test]

        public void validateLoginSuccess()
        {
            ClinicRepository clinicRepository = new ClinicRepository();
            bool actualres = clinicRepository.loginScreen("dustin20", "dustbun@12");
            bool expres = false;
            Assert.AreEqual(expres, actualres);
        }

        // ----------------------- Testing adding new patients -----------------------


    }
}