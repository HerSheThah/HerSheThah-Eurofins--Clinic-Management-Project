namespace cliniclibrary
{
    public class LoginScreen
    {
        public class PasswordException : ApplicationException
        {
            public PasswordException(string message) : base(message) { }
        }

        public class UsernameException : ApplicationException { 
            public UsernameException(string message) : base(message) { }
        }

        public string userName { get; set; }
        public string password { get; set; }

        public LoginScreen() { }
        public LoginScreen(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

    }
}