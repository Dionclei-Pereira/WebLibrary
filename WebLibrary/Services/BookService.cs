using Microsoft.EntityFrameworkCore;
using WebLibrary.Data;
using WebLibrary.DTO;
using WebLibrary.Entities;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services {
    public class BookService : IBookService {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context) {
            _context = context;
        }

        public async Task<BookDTO> GetBookById(int id) {
            var book = await _context.Books.AsNoTracking().Where(b => b.Id == id).FirstOrDefaultAsync();
            return book?.ToDto();
        }

        public async Task<List<BookDTO>> GetBooks() {
            return await _context.Books.AsNoTracking().Select(b => b.ToDto()).ToListAsync();
        }

        public async Task<BookDTO> Insert(Book book) {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return await GetBookById(book.Id);
        }
    }
}
