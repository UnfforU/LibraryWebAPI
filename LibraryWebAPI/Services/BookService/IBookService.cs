namespace LibraryWebAPI.Services.BookService
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksInLibrary(Guid libraryId);
        Task<Book?> GetBookById(Guid bookId);
        Task<Book?> AddBook(Book book);
        Task<Book?> UpdateBook(Guid bookId, Book request);
        Task<List<Book>> DeleteBook(Guid bookId);
    }
}
