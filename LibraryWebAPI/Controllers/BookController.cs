using LibraryWebAPI.Helpers;
using LibraryWebAPI.Services.BookService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace LibraryWebAPI.Controllers
{
    [Route("/Books")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICryptographyHelper _cryptoHelper;
        public BookController(IBookService bookService, ICryptographyHelper cryptographyHelper)
        {
            _bookService = bookService;
            _cryptoHelper = cryptographyHelper;
        }

        [HttpGet("{libraryId}")]
        [Authorize]
        public async Task<ActionResult<List<BookDTO>>> GetBooksByLibraryId(Guid libraryId)
        {
            var result = await _bookService.GetBooksByLibraryIdAsync(libraryId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDTO>> AddBook(BookDTO book)
        {
            var newBook = await _bookService.AddBookAsync(book);
            return Ok(newBook);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDTO>> UpdateBook(Guid id, BookDTO book)
        {
            var result = await _bookService.UpdateBookAsync(id, book);
            if(result is not null)
                return Ok(result);
            else
                return BadRequest("Can't update book.");

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(Guid id) =>
            await _bookService.DeleteBookAsync(id) ? NoContent() : NotFound("Book not found.");
    }
}
