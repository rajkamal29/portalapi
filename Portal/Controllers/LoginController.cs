using Microsoft.AspNetCore.Mvc;
using Portal.Model;
using Portal.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
       
        // POST api/<LoginController>
        [HttpPost]
        [Route("me")]
        public async Task<IActionResult> AuthenticateUser([FromBody] Login UserLogin)
        {
            if (UserLogin == null)
                return BadRequest("Invalid login details");
            if (string.IsNullOrEmpty(UserLogin.Email) || string.IsNullOrEmpty(UserLogin.Password))
                return BadRequest("Invalid Username or Password");

            return Ok(await _loginService.Authenticate(UserLogin));
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUpUser([FromBody] SignUp UserDetails)
        {
            if (UserDetails == null)
                return BadRequest("Invalid user details");
            if (string.IsNullOrEmpty(UserDetails.Email) || string.IsNullOrEmpty(UserDetails.Password))
                return BadRequest("Username and Password is required");
            await _loginService.SignUp(UserDetails);
            return Ok(new UserDetails { Firstname = UserDetails.Firstname, Lastname = UserDetails.Lastname, Email = UserDetails.Email });
        }
    }
}
