using WebLibrary.Data;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;

namespace WebLibrary.Services.Interfaces {
    public interface IBookService {
        Task<List<BookDTO>> GetBooks();
        Task<BookDTO> GetBookById(int id);
        Task<BookDTO> Insert(Book book);

    }
}
