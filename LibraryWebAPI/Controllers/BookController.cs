using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebAPI.Services.BookService;
using LibraryWebAPI.Services.LibraryService;
using LibraryWebAPI.Models.DB;

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

        //GET: api/Book/guid
        [HttpGet("{libraryId}")]
        public async Task<ActionResult<List<BookDTO>>> GetBooksInLibrary(Guid libraryId)
        {
            return await _bookService.GetAllBooksInLibrary(libraryId);
        }

        //// GET: api/Book/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Book>> GetBook(Guid id)
        //{
        //  if (_context.Books == null)
        //  {
        //      return NotFound();
        //  }
        //    var book = await _context.Books.FindAsync(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        // POST: api/Book
        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBook(BookDTO book)
        {
            await _bookService.AddBook(book);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Book/5
        [HttpPut("{bookId}")]
        public async Task<ActionResult<BookDTO>> UpdateBook(Guid bookId, BookDTO book)
        {
            await _bookService.UpdateBook(bookId, book);

            return NoContent();
        }

        // DELETE: api/Book/guid
        [HttpDelete("{bookId}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(Guid bookId)
        {
            var result = await _bookService.DeleteBook(bookId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        //private bool BookExists(Guid id)
        //{
        //    return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        //}
    }
}
