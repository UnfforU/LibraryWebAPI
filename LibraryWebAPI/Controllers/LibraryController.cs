using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Models.Extra;
using LibraryWebAPI.Services.LibraryService;
using Microsoft.AspNetCore.Authorization;

namespace LibraryWebAPI.Controllers
{
    [Route("/Libraries")]
    [ApiController]
    [Authorize]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public LibraryController(ILibraryService libaryService)
        {
            _libraryService = libaryService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<LibraryDTO>>> GetLibraries()
        {
            var result = await _libraryService.GetLibrariesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<LibraryDTO>> GetLibraryById(Guid id)
        {
            var result = await _libraryService.GetLibraryByIdAsync(id);
            if (result is null)
                return NotFound("Library not found.");

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LibraryDTO>> AddLibrary(LibraryDTO library)
        {
            var result = await _libraryService.AddLibraryAsync(library);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(Guid id) =>
            await _libraryService.DeleteLibraryAsync(id) ? NoContent() : NotFound("Library not found.");
    }
}
