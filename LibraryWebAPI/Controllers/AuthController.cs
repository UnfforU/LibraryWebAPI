using LibraryWebAPI.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LibraryWebAPI.Controllers
{
    [Route("/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public ActionResult Login(LoginDTO login)
        {
            var token = _authService.AuthenticateUser(login);
            if(token == string.Empty)
                return Unauthorized();

            return Ok(new
            {
                access_token = token
            });
        }
    }
}
