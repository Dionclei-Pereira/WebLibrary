using WebLibrary.DTO;
using WebLibrary.Entities;

namespace WebLibrary.Services.Interfaces {
    public interface IUserService {
        Task<List<UserDTO>> GetUsersDTO();
        Task<UserDTO> GetUserByEmail(string email);
        Task<List<LoanDTOWithoutUser>> GetUserLoansByEmail(string email);
        Task<UserDTO> Insert(User user);
    }
}
