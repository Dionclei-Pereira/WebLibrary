using WebLibrary.Data;
using WebLibrary.DTO;

namespace WebLibrary.Services.Interfaces {
    public interface IBookService {
        Task<List<BookDTO>> GetBooks();
        Task<BookDTO> GetBookById(int id);

    }
}
