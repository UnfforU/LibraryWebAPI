using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Services.BookService;
using LibraryWebAPI.Services.AuthorService;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryWebAPI.Controllers
{
    [Route("/Authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> AddAuthor(AuthorDTO author)
        {
            var newAuthor = await _authorService.AddAuthorAsync(author);
            return Ok(newAuthor);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var isDeleted = await _authorService.DeleteAuthorAsync(id);
            return isDeleted ? NoContent() : NotFound();
        }

    }
}
