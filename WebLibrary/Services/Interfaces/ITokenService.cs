using WebLibrary.Entities;
using WebLibrary.Entities.DTO;

namespace WebLibrary.Services.Interfaces {
    public interface ITokenService {

        Task<TokenDTO> GenerateToken(User user);

        Task<TokenResult> ValidateToken(string token);

    }
}
