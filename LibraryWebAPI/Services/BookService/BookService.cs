using LibraryWebAPI.Services.AuthorService;
using System.Runtime.CompilerServices;

namespace LibraryWebAPI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;
        private readonly IAuthorService _authorService;


        public BookService(LibraryContext context, IAuthorService authorService)
        {
            this._context = context;
            _authorService = authorService;
        }

        public async Task<BookDTO?> AddBook(BookDTO book)
        {
            var author = _authorService.AddAuthor(new Author { AuthorId = Guid.NewGuid(), Name = book.AuthorName });

            var newBook = new Book()
            {
                BookId = Guid.NewGuid(),
                Name = book.Name,
                AuthorId = author.Result.AuthorId,
                Description = book.Description,
                IsBooked = book.IsBooked,
                OwnerId = book.OwnerId,
                BookedDate = book.BookedDate,
                LibraryId = book.LibraryId,
            };


            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return book;
        }

        public Task<List<BookDTO>> DeleteBook(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookDTO>> GetAllBooksInLibrary(Guid libraryId)
        {
            var books = await _context.Books.Where(book => book.IsDeleted == null && book.LibraryId == libraryId).ToListAsync();

            var result = new List<BookDTO>();
            foreach (var book in books)
            {
                result.Add(new BookDTO()
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    AuthorName = _context.Authors.Find(book.AuthorId).Name.ToString(),
                    Description = book.Description,
                    IsBooked = book.IsBooked,
                    OwnerId = book.OwnerId,
                    BookedDate = book.BookedDate,
                    LibraryId = book.LibraryId,
                });
            }
            return result;
        }

        public Task<BookDTO?> GetBookById(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task<BookDTO?> UpdateBook(Guid bookId, BookDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
