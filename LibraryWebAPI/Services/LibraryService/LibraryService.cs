namespace LibraryWebAPI.Services.LibraryService
{
    public class LibraryService : ILibraryService
    {

        private readonly LibraryContext _context;
        public LibraryService(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<List<Library>> AddLibrary(Library library)
        {
            _context.Libraries.Add(library);
            await _context.SaveChangesAsync();
            return await _context.Libraries.ToListAsync();
        }

        public List<Library> DeleteLibrary(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Library>> GetAll()
        {
            var libraries = await _context.Libraries.ToListAsync();
            return libraries;
        }

        public Library GetById(Guid guid)
        {
            throw new NotImplementedException();
        }

        public List<Library> UpdateLibrary(Guid guid, Library library)
        {
            throw new NotImplementedException();
        }
    }
}
