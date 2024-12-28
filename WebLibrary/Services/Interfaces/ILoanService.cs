using WebLibrary.DTO;

namespace WebLibrary.Services.Interfaces {
    public interface ILoanService {

        Task<List<LoanDTO>> GetLoans();

    }
}
