using AutoMapper;
using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Services.AuthorBookService;
using LibraryWebAPI.Services.AuthorService;
using NuGet.LibraryModel;

namespace LibraryWebAPI.Services.FileLoaderService
{
    public class FileLoaderService : IFileLoaderService
    {
        private readonly WebLibraryDbContext _context;
        private readonly IMapper _mapper;

        public FileLoaderService(WebLibraryDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<bool> UploadFileAsync(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                Models.DB.File newFile = new()
                {
                    FileId = Guid.NewGuid(),
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FileContent = ms.GetBuffer(),
                    BookId = null
                };

                _context.Files.Add(newFile);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<FileDTO> GetFileByBookId(Guid bookId)
        {
            var outputFile = await _context.Files.FirstOrDefaultAsync(f => f.BookId == bookId);
            var a = 12;

            return _mapper.Map<FileDTO>(outputFile);
        }
    }
}
