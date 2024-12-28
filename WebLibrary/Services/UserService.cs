using Microsoft.EntityFrameworkCore;
using WebLibrary.Data;
using WebLibrary.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services {
    public class UserService : IUserService {

        private readonly LibraryContext _context;
        public UserService(LibraryContext libraryContext) {
            _context = libraryContext;
        }

        public async Task<UserDTO> GetUserByEmail(string email) {
            var user = await _context.Users.Where(u => u.Email == email).AsNoTracking().FirstOrDefaultAsync();
            return user?.ToDTO();
        }

        public async Task<List<LoanDTOWithoutUser>> GetUserLoansByEmail(string email) {
            var user = await _context.Users.AsNoTracking().Where(u => u.Email == email).Include(u => u.Loans).ThenInclude(l => l.Book).FirstOrDefaultAsync();
            return user?.Loans.Select(l => l.ToDTOWithoutUser()).ToList();
        
        }

        public async Task<List<UserDTO>> GetUsersDTO() {
            return await _context.Users.AsNoTracking().Select(User => User.ToDTO()).ToListAsync();
        }
    }
}
