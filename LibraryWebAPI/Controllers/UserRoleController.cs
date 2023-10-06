using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Services.UserRoleService;

namespace LibraryWebAPI.Controllers
{
    [Route("/UserRoles")]
    [ApiController]
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
