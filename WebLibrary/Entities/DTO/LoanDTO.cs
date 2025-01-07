namespace WebLibrary.Entities.DTO {
    public class LoanDTO {

        public int Id { get; set; }
        public UserDTO User { get; set; }
        public BookDTO Book { get; set; }

        public DateTime DateInit { get; set; }
        public DateTime DateBack { get; set; }

        public LoanDTO() { }

        public LoanDTO(UserDTO user, BookDTO book, DateTime dateInit, DateTime dateBack) {
            User = user;
            Book = book;
            DateInit = dateInit;
            DateBack = dateBack;
        }

    }
}
