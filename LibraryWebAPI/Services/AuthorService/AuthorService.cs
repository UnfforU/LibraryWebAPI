using LibraryWebAPI.Models.DB;

namespace LibraryWebAPI.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly WebLibraryDbContext _context;
        public AuthorService(WebLibraryDbContext context)
        {
            this._context = context;
        }
        public async Task<Author?> AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return await _context.Authors.FindAsync(author.AuthorId);
        }
    }
}
