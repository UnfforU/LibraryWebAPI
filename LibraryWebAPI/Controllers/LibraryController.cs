using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWebAPI.Services.LibraryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public LibraryController(ILibraryService libaryService)
        {
            _libraryService = libaryService;
        }

        // GET: api/Library
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Library>>> GetLibraries()
        {
            return await _libraryService.GetAll();
        }

        // GET: api/Library/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Library>> GetLibrary(Guid id)
        //{
        //  if (_context.Libraries == null)
        //  {
        //      return NotFound();
        //  }
        //    var library = await _context.Libraries.FindAsync(id);

        //    if (library == null)
        //    {
        //        return NotFound();
        //    }

        //    return library;
        //}

        //// PUT: api/Library/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateLibrary(Guid id, Library library)
        //{
        //    if (id != library.LibraryId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(library).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LibraryExists(id))
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

        // POST: api/Library
        [HttpPost]
        public async Task<ActionResult<Library>> AddLibrary(Library library)
        {
            int a = 10;
            var result = await _libraryService.AddLibrary(library);
            return Ok(result);
        }

        //    // DELETE: api/Library/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteLibrary(Guid id)
        //    {
        //        if (_context.Libraries == null)
        //        {
        //            return NotFound();
        //        }
        //        var library = await _context.Libraries.FindAsync(id);
        //        if (library == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Libraries.Remove(library);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool LibraryExists(Guid id)
        //    {
        //        return (_context.Libraries?.Any(e => e.LibraryId == id)).GetValueOrDefault();
        //    }
    }
}
