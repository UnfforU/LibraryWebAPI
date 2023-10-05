using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebAPI.Models.DTO;
using LibraryWebAPI.Services.LibraryService;
using LibraryWebAPI.Services.UserService;
using NuGet.LibraryModel;
using LibraryWebAPI.Models.DB;
using Microsoft.AspNetCore.Http.HttpResults;

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
            return await _userService.AddUserAsync(userDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var isDeleted = await _userService.DeleteUserAsync(id);
            return isDeleted ? NoContent() : NotFound();
        }
    }
}
