using AutoMapper;
using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Services.BookService;
using Microsoft.VisualBasic;

namespace LibraryWebAPI.Services.LibraryService
{
    public class LibraryService : ILibraryService
    {
        private readonly WebLibraryDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService; 
        public LibraryService(WebLibraryDbContext context, IMapper mapper, IBookService bookService)
        {
            this._context = context;
            this._mapper = mapper;
            this._bookService = bookService;
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

            var updLibraryMapped = _mapper.Map<Library>(library);

            //Nedded cause i can't udpLibrary by "= _mapper.Map()"
            _context.Entry(updLibrary).CurrentValues.SetValues(updLibraryMapped);
            await _context.SaveChangesAsync();


            return _mapper.Map<LibraryDTO>(updLibrary);
        }

        public async Task<bool> DeleteLibraryAsync(Guid id)
        {
            var library = await _context.Libraries.FindAsync(id);
            if (library is null)
                return false;

            library.IsDeleted = true;
            if(await _bookService.DeleteListOfBooksAsync(library.Books.Select(book => book.BookId).ToArray()))
            {
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
