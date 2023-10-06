using LibraryWebAPI.Services.UserService;

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

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser(UserDTO userDTO)
        {
            try
            {
                return await _userService.AddUserAsync(userDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id) => 
            await _userService.DeleteUserAsync(id) ? NoContent() : NotFound();
    
    }
}
