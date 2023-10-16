using LibraryWebAPI.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace LibraryWebAPI.Controllers
{
    [Route("/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            var result = await _userService.GetUsersAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserDTO>> AddUser(UserDTO userDTO)
        {
            var result = await _userService.AddUserAsync(userDTO);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id) => 
            await _userService.DeleteUserAsync(id) ? NoContent() : NotFound();
    
    }
}
