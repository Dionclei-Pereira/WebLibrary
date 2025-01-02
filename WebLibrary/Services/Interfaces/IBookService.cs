using WebLibrary.Data;
using WebLibrary.DTO;
using WebLibrary.Entities;

namespace WebLibrary.Services.Interfaces {
    public interface IBookService {
        Task<List<BookDTO>> GetBooks();
        Task<BookDTO> GetBookById(int id);
        Task<BookDTO> Insert(Book book);

    }
}
