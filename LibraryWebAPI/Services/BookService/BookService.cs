namespace LibraryWebAPI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;
        public BookService(LibraryContext context)
        {
            this._context = context;
        }

        public Task<Book?> AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> DeleteBook(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetAllBooksInLibrary(Guid libraryId)
        {
            var books = await _context.Books.Where(book => book.Library == libraryId).ToListAsync();
            return books;
        }

        public Task<Book?> GetBookById(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> UpdateBook(Guid bookId, Book request)
        {
            throw new NotImplementedException();
        }
    }
}
