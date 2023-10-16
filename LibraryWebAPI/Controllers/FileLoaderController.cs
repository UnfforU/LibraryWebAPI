using LibraryWebAPI.Helpers;
using LibraryWebAPI.Services.BookService;
using LibraryWebAPI.Services.FileLoaderService;
using Microsoft.AspNetCore.Authorization;

namespace LibraryWebAPI.Controllers
{
    [Route("/Files")]
    [ApiController]
    [Authorize]
    public class FileLoaderController : ControllerBase
    {
        private readonly IFileLoaderService _fileLoaderService;
        private readonly ICryptographyHelper _cryptoHelper;
        public FileLoaderController(IFileLoaderService fileLoaderService, ICryptographyHelper cryptographyHelper)
        {
            _fileLoaderService = fileLoaderService;
            _cryptoHelper = cryptographyHelper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> UploadFile(IFormFile file)
        {
            var res = await _fileLoaderService.UploadFileAsync(file);
            return Ok(res);
        }

        [HttpGet("{bookId}")]
        [Authorize]
        public async Task<ActionResult<FileDTO>> GetFileByBookId(Guid bookId)
        {
            var res = await _fileLoaderService.GetFileByBookId(bookId);
            return res;
        }
    }
}
