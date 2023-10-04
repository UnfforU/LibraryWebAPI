using LibraryWebAPI.Models.DB;
using Microsoft.VisualBasic;

namespace LibraryWebAPI.Services.LibraryService
{
    public class LibraryService : ILibraryService
    {

        private readonly WebLibraryDbContext _context;
        public LibraryService(WebLibraryDbContext context)
        {
            this._context = context;
        }

        public async Task<Library?> AddLibrary(Library library)
        {
            _context.Libraries.Add(library);
            await _context.SaveChangesAsync();
            return await _context.Libraries.FindAsync(library.LibraryId);
        }

        public async Task<List<Library>> DeleteLibrary(Guid libraryId)
        {
            var library = await _context.Libraries.FindAsync(libraryId);
            if (library is null)
                return null;

            library.IsDeleted = true;
            _context.Entry(library).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.Libraries.Where(l => l.IsDeleted == null).ToListAsync();
        }

        public async Task<List<Library>> GetAllLibraries()
        {
            var libraries = await _context.Libraries.Where(l => l.IsDeleted == null).ToListAsync();
            return libraries;
        }

        public async Task<Library?> GetLibraryById(Guid libraryId)
        {
            var library = await _context.Libraries.FindAsync(libraryId);
            return library;
        }

        public async Task<Library?> UpdateLibrary(Guid libraryId, Library request)
        {
            var library = await _context.Libraries.FindAsync(libraryId);
            if (library is null)
                return null;

            library.Name = request.Name;
            library.IsDeleted = request.IsDeleted;

            await _context.SaveChangesAsync();

            return await _context.Libraries.FindAsync(libraryId);
        }
    }
}
