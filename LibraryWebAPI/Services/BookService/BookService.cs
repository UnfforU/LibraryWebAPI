using AutoMapper;
using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Services.AuthorBookService;
using LibraryWebAPI.Services.AuthorService;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.LibraryModel;
using System.Runtime.CompilerServices;

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

            _context.Books.Add(newBook);
            foreach(var author in book.Authors)
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

        public async Task<List<BookDTO>> GetBooksByLibraryIdAsync(Guid libraryId) =>
            _mapper.Map<List<BookDTO>>(await _context.Books.Where(b => b.LibraryId == libraryId).ToListAsync());

        public async Task<BookDTO> UpdateBookAsync(Guid id, BookDTO book)
        {
            var updBook = await _context.Books.FindAsync(id);
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
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return false;

            book.IsDeleted = true;

            //Besides deleted book, i should delete AuthorBooks rows, which connected with deleted book
            foreach(var a in _context.AuthorBooks.Where(ab => ab.BookId == book.BookId).ToList())
            {
                a.IsDeleted = true;
            }

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
