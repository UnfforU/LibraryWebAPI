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

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //POST api/User
        [HttpPost]
        public async Task<List<User>> AddUser(UserDTO userDTO)
        {
            return await _userService.AddUser(userDTO);
        }

        //DELETE: api/User/5
        [HttpDelete("{userId}")]
        public async Task<ActionResult<List<User>>> DeleteUser(Guid userId)
        {
            var result = await _userService.DeleteUser(userId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }
    }
}
