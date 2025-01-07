namespace WebLibrary.Entities.DTO {
    public record LoanDTO(UserDTO User, BookDTO Book, DateTime DateInit, DateTime DateBack) {
    }
}
