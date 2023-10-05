using AutoMapper;
using LibraryWebAPI.Models.DB;
using Microsoft.VisualBasic;

namespace LibraryWebAPI.Services.LibraryService
{
    public class LibraryService : ILibraryService
    {
        private readonly WebLibraryDbContext _context;
        private readonly IMapper _mapper;
        public LibraryService(WebLibraryDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<LibraryDTO> AddLibraryAsync(LibraryDTO library)
        {
            var newLibrary = _mapper.Map<Library>(library);

            _context.Libraries.Add(newLibrary);
            await _context.SaveChangesAsync();

            return _mapper.Map<LibraryDTO>(newLibrary);
        }

        public async Task<List<LibraryDTO>> GetLibrariesAsync() =>
            _mapper.Map<List<LibraryDTO>>(await _context.Libraries.ToListAsync());
        

        public async Task<LibraryDTO> GetLibraryByIdAsync(Guid id) =>
            _mapper.Map<LibraryDTO>(await _context.Libraries.FindAsync(id));


        public async Task<LibraryDTO> UpdateLibraryAsync(Guid id, LibraryDTO library)
        {
            var updLibrary = await _context.Libraries.FindAsync(id);
            if (updLibrary is null)
                throw new Exception("Library not found");

            updLibrary = _mapper.Map<Library>(library);
            await _context.SaveChangesAsync();

            return _mapper.Map<LibraryDTO>(updLibrary);
        }

        public async Task<bool> DeleteLibraryAsync(Guid id)
        {
            var library = await _context.Libraries.FindAsync(id);
            if (library is null)
                return false;

            library.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
