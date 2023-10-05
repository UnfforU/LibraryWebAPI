using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Services.LibraryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace LibraryWebAPI.Controllers
{
    [Route("/Libraries")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public LibraryController(ILibraryService libaryService)
        {
            _libraryService = libaryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibraryDTO>>> GetLibraries()
        {
            var result = await _libraryService.GetLibrariesAsync();
            return Ok(result);
        }

        [HttpGet("{libraryId}")]
        public async Task<ActionResult<LibraryDTO>> GetLibraryById(Guid libraryId)
        {
            var result = await _libraryService.GetLibraryByIdAsync(libraryId);
            if (result is null)
                return NotFound("Library not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LibraryDTO>> AddLibrary(LibraryDTO library)
        {
            var result = await _libraryService.AddLibraryAsync(library);
            return Ok(result);
        }

        [HttpPut("{libraryId}")]
        public async Task<ActionResult<LibraryDTO>> UpdateLibrary(Guid id, LibraryDTO library)
        {
            try
            {
                var result = await _libraryService.UpdateLibraryAsync(id, library);
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound("Library not found.");
            }
        }

        [HttpDelete("{libraryId}")]
        public async Task<ActionResult<List<Library>>> DeleteLibrary(Guid libraryId) =>
            await _libraryService.DeleteLibraryAsync(libraryId) ? NoContent() : NotFound("Library not found.");
    }
}
