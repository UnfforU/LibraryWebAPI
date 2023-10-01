using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<ActionResult<List<Library>>> GetLibraries()
        {
            return await _libraryService.GetAllLibraries();
        }

        // GET: api/Library/guid
        [HttpGet("{libraryId}")]
        public async Task<ActionResult<Library>> GetLibraryById(Guid libraryId)
        {
            var result = await _libraryService.GetLibraryById(libraryId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        // PUT: api/Library/guid
        [HttpPut("{libraryId}")]
        public async Task<ActionResult<Library>> UpdateLibrary(Guid libraryId, Library request)
        {
            var result = await _libraryService.UpdateLibrary(libraryId, request);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        // POST: api/Library
        [HttpPost]
        public async Task<ActionResult<Library>> AddLibrary(Library library)
        {
            var result = await _libraryService.AddLibrary(library);
            return Ok(result);
        }

        // DELETE: api/Library/guid
        [HttpDelete("{libraryId}")]
        public async Task<ActionResult<Library>> DeleteLibrary(Guid libraryId)
        {
            var result = await _libraryService.DeleteLibrary(libraryId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }
    }
}
