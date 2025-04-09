using WebLibrary.Entities;
using WebLibrary.Entities.DTO;

namespace WebLibrary.Services.Interfaces {
    public interface ITokenService {

        TokenDTO GenerateToken(User user);

        TokenResult ValidateToken(string token);

    }
}
