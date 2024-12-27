using WebLibrary.Data;
using WebLibrary.DTO;

namespace WebLibrary.Services.Interfaces {
    public interface IBookService {
        public List<BookDTO> GetBooks();

    }
}
