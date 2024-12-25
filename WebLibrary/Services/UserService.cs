using WebLibrary.Data;
using WebLibrary.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services {
    public class UserService : IUserService {

        private readonly LibraryContext _context;
        public UserService(LibraryContext libraryContext) {
            _context = libraryContext;
        }

        public List<UserDTO> GetUsersDTO() {
            return _context.Users.Select(User => User.ToDTO()).ToList();
        }

    }
}
