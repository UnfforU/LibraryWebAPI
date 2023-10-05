using AutoMapper;

namespace LibraryWebAPI.Services.AuthorBookService
{
    public class AuthorBookService : IAuthorBookService
    {
        private readonly WebLibraryDbContext _context;
        private readonly IMapper _mapper;
        public AuthorBookService(WebLibraryDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<List<AuthorDTO>> GetAuthorsByBookIdAsync(Guid bookId)
        {
            var result = await _context.AuthorBooks.Where(ab => ab.BookId == bookId).ToListAsync();
            return _mapper.Map<List<AuthorDTO>>(result);
        }

        public async Task<List<BookDTO>> GetBooksByAthorIdAsync(Guid athorId)
        {
            var result = await _context.AuthorBooks.Where(ab => ab.AuthorId == athorId).ToListAsync();
            return _mapper.Map<List<BookDTO>>(result);
        }
    }
}
