using LibraryWebAPI.Services.BookService;

namespace LibraryWebAPI.Controllers
{
    [Route("/Books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{libraryId}")]
        public async Task<ActionResult<List<BookDTO>>> GetBooksByLibraryId(Guid libraryId)
        {
            var result = await _bookService.GetBooksByLibraryIdAsync(libraryId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBook(BookDTO book)
        {
            await _bookService.AddBookAsync(book);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> UpdateBook(Guid id, BookDTO book)
        {
            try
            {
                var result = await _bookService.UpdateBookAsync(id, book);
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound("Library not found.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(Guid id) =>
            await _bookService.DeleteBookAsync(id) ? NoContent() : NotFound("Library not found.");
    }
}
