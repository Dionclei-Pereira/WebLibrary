using Microsoft.EntityFrameworkCore;
using WebLibrary.Data;
using WebLibrary.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services {
    public class LoanService : ILoanService {

        private readonly LibraryContext _context;

        public LoanService(LibraryContext context) {
            _context = context;
        }

        public async Task<List<LoanDTO>> GetLoans() {
            return await _context.Loans.AsNoTracking().Include(l => l.User).Include(l => l.Book).Select(l => l.ToDTO()).ToListAsync();
        }
    }
}
