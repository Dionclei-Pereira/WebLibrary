using WebLibrary.Entities;
using WebLibrary.Entities.DTO;

namespace WebLibrary.Services.Interfaces {
    public interface IUserService {
        Task<List<UserDTO>> GetUsersDTO();
        Task<UserDTO> GetUserByEmail(string email);
        Task<List<LoanDTOWithoutUser>> GetUserLoansByEmail(string email);
        Task<UserDTO> Insert(User user);
        Task<double> AddPenalty(string email, double amount);
        Task<double> SetPenalty(string email, double amount);
        Task<double> ResetPenalty(string email);
        
    }
}
