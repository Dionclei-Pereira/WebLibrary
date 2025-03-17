using Microsoft.EntityFrameworkCore;
using WebLibrary.Data;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services {
    public class BookService : IBookService {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context) {
            _context = context;
        }

        public async Task<BookDTO> GetBookById(int id) {
            var book = await _context.Books.AsNoTracking().Where(b => b.Id == id).Include(b => b.BookCategory).FirstOrDefaultAsync();
            return book?.ToDto();
        }

        public async Task<Book> GetBookByIdNoDTO(int id) {
            var book = await _context.Books.AsNoTracking().Where(b => b.Id == id).Include(b => b.BookCategory).Include(b => b.Loan).FirstOrDefaultAsync();
            return book;
        }

        public async Task<List<BookDTO>> GetBooks() {
            return await _context.Books.AsNoTracking().Include(b => b.BookCategory).Select(b => b.ToDto()).ToListAsync();
        }

        public async Task<BookDTO> Insert(Book book) {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return await GetBookById(book.Id);
        }
    }
}
