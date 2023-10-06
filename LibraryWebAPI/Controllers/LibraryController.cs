﻿using LibraryWebAPI.Services.LibraryService;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryDTO>> GetLibraryById(Guid id)
        {
            var result = await _libraryService.GetLibraryByIdAsync(id);
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Library>>> DeleteLibrary(Guid id) =>
            await _libraryService.DeleteLibraryAsync(id) ? NoContent() : NotFound("Library not found.");
    }
}
