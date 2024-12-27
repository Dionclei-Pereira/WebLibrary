using WebLibrary.Data;
using WebLibrary.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services {
    public class BookService : IBookService {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context) {
            _context = context;
        }

        public List<BookDTO> GetBooks() {
            return _context.Books.Select(b => b.ToDto()).ToList();
        }
    }
}
