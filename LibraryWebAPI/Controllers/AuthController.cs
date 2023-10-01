using LibraryWebAPI.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(Login request)
        {
            var token =  _authService.AuthenticateUser(request);
            if(token == string.Empty)
                return Unauthorized();

            return Ok(new
            {
                access_token = token
            });
        }
    }
}
