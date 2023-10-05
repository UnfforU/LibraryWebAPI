namespace LibraryWebAPI.Services.AuthorBookService
{
    public interface IAuthorBookService
    {
        Task<List<BookDTO>> GetBooksByAthorIdAsync(Guid athorId);
        Task<List<AuthorDTO>> GetAuthorsByBookIdAsync(Guid bookId);
    }
}
