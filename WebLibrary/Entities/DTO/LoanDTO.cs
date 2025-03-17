namespace WebLibrary.Entities.DTO {
    public record LoanDTO(int LoanId, UserDTO User, BookDTO Book, DateTime DateInit, DateTime DateBack) {
    }
}
