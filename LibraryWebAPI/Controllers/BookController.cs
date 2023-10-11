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
            if (newBook != null)
            {
                return Ok(newBook);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDTO>> UpdateBook(Guid id, BookDTO book)
        {
            try
            {
                var result = await _bookService.UpdateBookAsync(id, book);
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound("Book not found.");
            }
        }

        //"Put" method for subscribe on book with getting userId from JWT token(optional method)
        //[HttpPut]
        //public async Task<ActionResult<BookDTO>> SubscribeOnBook(Guid id, BookDTO book)
        //{
        //    Request.Headers.TryGetValue("Authorization", out StringValues strv);
        //    var jwt = strv.ToString().Split(" ")[1];
        //    var claims = _cryptoHelper.DecodeJWT(jwt).Claims;

        //    var currUserId = claims.First(claim => claim.Type == "sub").Value;
        //    book.OwnerId = new Guid(currUserId);

        //    try
        //    {
        //        var result = await _bookService.UpdateBookAsync(id, book);
        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        return NotFound("Book not found.");
        //    }
        //}

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(Guid id) =>
            await _bookService.DeleteBookAsync(id) ? NoContent() : NotFound("Book not found.");
    }
}
