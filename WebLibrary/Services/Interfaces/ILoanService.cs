using WebLibrary.Entities;
using WebLibrary.Entities.DTO;


namespace WebLibrary.Services.Interfaces {
    public interface ILoanService {

        Task<Loan> AddLoan(User user, Book book);
        Task RemoveLoan(int loanId);
        Task<List<LoanDTO>> GetLoans();
        Task<LoanDTO> GetLoanById(int loanId);
    }
}
