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
        public async Task<ActionResult<List<Library>>> GetLibraries()
        {
            return await _libraryService.GetAllLibraries();
        }

        [HttpGet("{libraryId}")]
        public async Task<ActionResult<Library>> GetLibraryById(Guid libraryId)
        {
            var result = await _libraryService.GetLibraryById(libraryId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Library>> AddLibrary(Library library)
        {
            var result = await _libraryService.AddLibrary(library);
            return Ok(result);
        }

        [HttpPut("{libraryId}")]
        public async Task<ActionResult<Library>> UpdateLibrary(Guid libraryId, Library request)
        {
            var result = await _libraryService.UpdateLibrary(libraryId, request);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        [HttpDelete("{libraryId}")]
        public async Task<ActionResult<List<Library>>> DeleteLibrary(Guid libraryId)
        {
            var result = await _libraryService.DeleteLibrary(libraryId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }
    }
}
