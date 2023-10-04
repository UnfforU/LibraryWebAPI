using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Services.AuthorService;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;

namespace LibraryWebAPI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly WebLibraryDbContext _context;
        private readonly IAuthorService _authorService;


        public BookService(WebLibraryDbContext context, IAuthorService authorService)
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
                //AuthorId = author.Result.AuthorId,
                Description = book.Description,
                IsBooked = book.IsBooked,
                OwnerId = book.OwnerId,
                BookedDate = book.BookedDate,
                LibraryId = book.LibraryId,
            };

            book.BookId = newBook.BookId;

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<List<BookDTO>> DeleteBook(Guid bookId)
        {
            var deletedBook = await _context.Books.FindAsync(bookId);
            if (deletedBook is null)
                return null;

            var libraryId = deletedBook.LibraryId;

            deletedBook.IsDeleted = true;
            _context.Entry(deletedBook).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var books = await _context.Books.Where(book => book.IsDeleted == null && book.LibraryId == libraryId).ToListAsync();

            var result = new List<BookDTO>();
            foreach (var book in books)
            {
                result.Add(new BookDTO()
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    //AuthorName = _context.Authors.Find(book.AuthorId).Name.ToString(),
                    Description = book.Description,
                    IsBooked = book.IsBooked,
                    OwnerId = book.OwnerId,
                    BookedDate = book.BookedDate,
                    LibraryId = book.LibraryId,
                });
            }

            return result;
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
                    //AuthorName = _context.Authors.Find(book.AuthorId).Name.ToString(),
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

        public async Task<BookDTO?> UpdateBook(Guid bookId, BookDTO request)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book is null)
                return null;
            
            book.BookId = request.BookId;
            book.Name = request.Name;
            //book.AuthorId = _context.Authors.First(au => au.Name == request.AuthorName).AuthorId;
            book.Description = request.Description;
            book.IsBooked = request.IsBooked;
            book.OwnerId = request.OwnerId;
            book.BookedDate = request.BookedDate;
            book.LibraryId = request.LibraryId;

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return  request;
        }
    }
}
