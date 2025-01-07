using Microsoft.AspNetCore.Identity;
using WebLibrary.Entities.DTO;

namespace WebLibrary.Entities {
    public class User : IdentityUser {
        public string Name { get; set; } = string.Empty;

        public List<Loan> Loans { get; set; }
        public double Penalty { get; set; }
        public UserDTO ToDTO() {
            return new UserDTO(Id, Name, Email, Penalty);
        }
    }
}
