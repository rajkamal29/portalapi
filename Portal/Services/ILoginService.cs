using Portal.Model;

namespace Portal.Services
{
    public interface ILoginService
    {
        Task<UserDetails> Authenticate(Login login);
        Task SignUp(SignUp userDetails);
    }
}
