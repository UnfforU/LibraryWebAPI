namespace LibraryWebAPI.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _context;
        public AuthorService(LibraryContext context)
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
