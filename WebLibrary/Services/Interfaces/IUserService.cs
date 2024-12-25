using WebLibrary.DTO;

namespace WebLibrary.Services.Interfaces {
    public interface IUserService {
        List<UserDTO> GetUsersDTO();
    }
}
