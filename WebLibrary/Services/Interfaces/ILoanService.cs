using WebLibrary.Entities.DTO;

namespace WebLibrary.Services.Interfaces {
    public interface ILoanService {

        Task<List<LoanDTO>> GetLoans();

    }
}
