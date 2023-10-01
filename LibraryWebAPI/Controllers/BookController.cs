using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebAPI.Models;
using LibraryWebAPI.Services.BookService;
using LibraryWebAPI.Services.LibraryService;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<Book>>> GetBooksInLibrary(Guid libraryId)
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

        //// PUT: api/Book/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(Guid id, Book book)
        //{
        //    if (id != book.BookId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Book
        //[HttpPost]
        //public async Task<ActionResult<Book>> PostBook(Book book)
        //{
        //  if (_context.Books == null)
        //  {
        //      return Problem("Entity set 'LibraryContext.Books'  is null.");
        //  }
        //    _context.Books.Add(book);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (BookExists(book.BookId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        //}

        //// DELETE: api/Book/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBook(Guid id)
        //{
        //    if (_context.Books == null)
        //    {
        //        return NotFound();
        //    }
        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool BookExists(Guid id)
        //{
        //    return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        //}
    }
}
