namespace Portal.Model
{
    public class Login
    {       
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class SignUp
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }

}
