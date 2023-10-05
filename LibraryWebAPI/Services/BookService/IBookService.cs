namespace LibraryWebAPI.Services.BookService
{
    public interface IBookService
    {
        Task<BookDTO?> GetBookByIdAsync(Guid id);
        Task<BookDTO> AddBookAsync(BookDTO book);
        Task<BookDTO> UpdateBookAsync(Guid id, BookDTO book);
        Task<bool> DeleteBookAsync(Guid id);
        Task<List<BookDTO>> GetBooksByLibraryIdAsync(Guid libraryId);
        
    }
}
