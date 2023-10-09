using LibraryWebAPI.Services.UserRoleService;
using Microsoft.AspNetCore.Authorization;

namespace LibraryWebAPI.Controllers
{
    [Route("/UserRoles")]
    [ApiController]
    [Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public ActionResult<UserRoleDTO> AddUserRole(UserRoleDTO userRole)
        {
            var newUserRole = _userRoleService.AddUserRole(userRole);
            return Ok(userRole);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserRole(Guid id) =>
            _userRoleService.DeleteUserRole(id) ? NoContent() : NotFound();
    }
}
