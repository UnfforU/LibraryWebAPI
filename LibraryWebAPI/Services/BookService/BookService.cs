using AutoMapper;
using LibraryWebAPI.Services.AuthorBookService;
using LibraryWebAPI.Services.AuthorService;
using LibraryWebAPI.Models.DB;

namespace LibraryWebAPI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly WebLibraryDbContext _context;
        private readonly IAuthorBookService _authorBookService;
        private readonly IMapper _mapper;

        public BookService(WebLibraryDbContext context, IAuthorService authorService, IMapper mapper, IAuthorBookService authorBookService)
        {
            this._context = context;
            this._mapper = mapper;
            this._authorBookService = authorBookService;
        }

        public async Task<BookDTO> AddBookAsync(BookDTO book)
        {
            var newBook = _mapper.Map<Book>(book);
            //newBook.OwnerId = null;

            _context.Books.Add(newBook);
            foreach (var author in book.Authors)
            {
                _context.AuthorBooks.Add(new AuthorBook() { AuthorId = author.AuthorId, BookId = newBook.BookId });
            }
            await _context.SaveChangesAsync();

            var newBookDTO = _mapper.Map<BookDTO>(newBook);
            newBookDTO.Authors = book.Authors;
            return newBookDTO;
        }

        public async Task<BookDTO?> GetBookByIdAsync(Guid id) =>
            _mapper.Map<BookDTO>(await _context.Books.FindAsync(id));

        public async Task<List<BookDTO>> GetBooksByLibraryIdAsync(Guid libraryId) 
        {
            var booksList = _mapper.Map<List<BookDTO>>(await _context.Books.Include(b => b.Orders).Where(b => b.LibraryId == libraryId).ToListAsync());

            //var a = ;

            //var b = from b in _context.Books.Include(b => b.AuthorBooks).Where(b => b.LibraryId == libraryId)
            //        join a in 

            //var result = booksList.Join()
            foreach(var book in booksList)
            {
                book.Authors = await _authorBookService.GetAuthorsByBookIdAsync(book.BookId);
            }
            return booksList;
        }
            

        public async Task<BookDTO> UpdateBookAsync(Guid id, BookDTO book)
        {
            var updBook = _context.Books.Include(b => b.AuthorBooks).FirstOrDefault(b => b.BookId == id);
            if (updBook is null)
                throw new Exception("Library not found");

            var updBookMapped = _mapper.Map<Book>(book);

            //Nedded cause i can't updBook by "= _mapper.Map()"
            _context.Entry(updBook).CurrentValues.SetValues(updBookMapped);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookDTO>(updBook);
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            var book = _context.Books.Include(b => b.AuthorBooks).Include(b => b.Orders).FirstOrDefault(b => b.BookId == id);
            if (book is null)
                return false;

            book.IsDeleted = true;
            book.Orders.ForEach(o => o.IsDeleted = true);
            book.AuthorBooks.ForEach(ab => ab.IsDeleted = true);

            //Besides deleted book, i should delete AuthorBooks rows, which connected with deleted book
            //foreach(var a in _context.AuthorBooks.Where(ab => ab.BookId == book.BookId).ToList())
            //{
            //    a.IsDeleted = true;
            //}

            await _context.SaveChangesAsync();
            return true;
        } 

        public async Task<bool> DeleteListOfBooksAsync(Guid[] ids)
        {
            var books = _context.Books.Where(bs => ids.Contains(bs.BookId)).ToList();
            if(books.Count != ids.Length)
            {
                return false;
            }

            foreach(var book in books)
            {
                book.IsDeleted = true;

                //Besides deleted book, i should delete AuthorBooks rows, which connected with deleted book
                foreach (var a in _context.AuthorBooks.Where(ab => ab.BookId == book.BookId).ToList())
                {
                    a.IsDeleted = true;
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
