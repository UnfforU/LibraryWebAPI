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
            var deletedAuthor = await _context.Authors.FindAsync(id);
            if(deletedAuthor != null)
            {
                _context.Authors.Remove(deletedAuthor);
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
