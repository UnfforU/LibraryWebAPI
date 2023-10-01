namespace LibraryWebAPI.Services.BookService
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllBooksInLibrary(Guid libraryId);
        Task<BookDTO?> GetBookById(Guid bookId);
        Task<BookDTO?> AddBook(BookDTO book);
        Task<BookDTO?> UpdateBook(Guid bookId, BookDTO request);
        Task<List<BookDTO>> DeleteBook(Guid bookId);
    }
}
