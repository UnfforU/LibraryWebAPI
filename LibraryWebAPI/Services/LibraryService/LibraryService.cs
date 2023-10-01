using Microsoft.VisualBasic;

namespace LibraryWebAPI.Services.LibraryService
{
    public class LibraryService : ILibraryService
    {

        private readonly LibraryContext _context;
        public LibraryService(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<Library?> AddLibrary(Library library)
        {
            _context.Libraries.Add(library);
            await _context.SaveChangesAsync();
            return await _context.Libraries.FindAsync(library.LibraryId);
        }

        public async Task<List<Library>> DeleteLibrary(Guid guid)
        {
            var library = await _context.Libraries.FindAsync(guid);
            if (library is null)
                return null;

            _context.Libraries.Remove(library);
            await _context.SaveChangesAsync();

            return await _context.Libraries.ToListAsync();
        }

        public async Task<List<Library>> GetAllLibraries()
        {
            var libraries = await _context.Libraries.ToListAsync();
            return libraries;
        }

        public async Task<Library?> GetLibraryById(Guid guid)
        {
            var library = await _context.Libraries.FindAsync(guid);
            return library;
        }

        public async Task<Library?> UpdateLibrary(Guid guid, Library request)
        {
            var library = await _context.Libraries.FindAsync(guid);
            if (library is null)
                return null;

            library.Name = request.Name;
            library.IsDeleted = request.IsDeleted;

            await _context.SaveChangesAsync();

            return await _context.Libraries.FindAsync(guid);
        }
    }
}
