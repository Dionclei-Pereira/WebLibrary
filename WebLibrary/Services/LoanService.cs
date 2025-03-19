using Microsoft.EntityFrameworkCore;
using WebLibrary.Data;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Exceptions;
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
        public async Task<Loan> AddLoan(User user, Book book) {
            _context.Attach(user);
            _context.Attach(book);
            if (book.Loan != null) {
                throw new LoanException("The book has already been borrowed.");
            }
            Loan loan = new Loan(user, book, DateTime.Now, DateTime.Now.AddDays(14));
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task RemoveLoan(int loanId) {
            var loan = await _context.Loans.AsNoTracking().FirstOrDefaultAsync(l => l.Id == loanId);
            if (loan == null) {
                return;
            }
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<LoanDTO> GetLoanById(int loanId) {
            var loan = _context.Loans.AsNoTracking().Include(l => l.User).Include(l => l.Book).FirstOrDefault(l => l.Id == loanId);
            if (loan == null) {
                return null;
            }
            return loan.ToDTO();
        }
        public async Task<LoanDTO> Renew(int loanId) {
            var loan = await _context.Loans.Where(l => l.Id == loanId).Include(l => l.User).Include(l => l.Book).FirstOrDefaultAsync() ?? throw new LoanException("Loan not Found");
            if (loan.DateBack < DateTime.Now.ToUniversalTime()) throw new LoanException("You can't renew a book that's overdue.");
            loan.DateBack = DateTime.Now.AddDays(15);
            loan.DateInit = DateTime.Now;
            await _context.SaveChangesAsync();
            return loan.ToDTO();
        }
    }
}
