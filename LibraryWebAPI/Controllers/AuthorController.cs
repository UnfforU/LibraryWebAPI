using LibraryWebAPI.Services.AuthorService;

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
        public async Task<IActionResult> DeleteAuthor(Guid id) =>
            await _authorService.DeleteAuthorAsync(id) ? NoContent() : NotFound();

    }
}
