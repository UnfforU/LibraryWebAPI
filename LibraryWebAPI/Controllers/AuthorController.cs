using AutoMapper;
using LibraryWebAPI.Services.AuthorService;
using Microsoft.AspNetCore.Authorization;

namespace LibraryWebAPI.Controllers
{
    [Route("/Authors")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<LibraryDTO>>> GetAuthorList()
        {
            var result = await _authorService.GetAuthorListAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AuthorDTO>> AddAuthor(AuthorDTO author)
        {
            var newAuthor = await _authorService.AddAuthorAsync(author);
            return Ok(newAuthor);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthor(Guid id) =>
            await _authorService.DeleteAuthorAsync(id) ? NoContent() : NotFound();

    }
}
