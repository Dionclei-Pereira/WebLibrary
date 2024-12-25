using Microsoft.AspNetCore.Identity;
using WebLibrary.DTO;

namespace WebLibrary.Entities {
    public class User : IdentityUser {
        public string Name { get; set; } = string.Empty;

        public UserDTO ToDTO() {
            return new UserDTO(Id, Name, Email);
        }
    }
}
