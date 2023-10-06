using AutoMapper;
using LibraryWebAPI.Models.DB;

namespace LibraryWebAPI.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly WebLibraryDbContext _context;
        private readonly IMapper _mapper;
        public AuthorService(WebLibraryDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<AuthorDTO> AddAuthorAsync(AuthorDTO author)
        {
            var newAuthor = _mapper.Map<Author>(author);

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorDTO>(newAuthor);
        }

        public async Task<bool> DeleteAuthorAsync(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author is null)
                return false;

            author.IsDeleted = true;

            //Besides deleted author, i should delete AuthorBooks rows, which connected with deleted author
            foreach (var a in _context.AuthorBooks.Where(ab => ab.AuthorId == author.AuthorId).ToList())
            {
                a.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
