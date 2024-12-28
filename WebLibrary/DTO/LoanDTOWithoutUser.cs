namespace WebLibrary.DTO {
    public class LoanDTOWithoutUser {

        public int Id { get; set; }
        public BookDTO Book { get; set; }
        public DateTime DateInit { get; set; }
        public DateTime DateBack { get; set; }

        public LoanDTOWithoutUser() { }

        public LoanDTOWithoutUser(BookDTO book, DateTime dateInit, DateTime dateBack) {
            Book = book;
            DateInit = dateInit;
            DateBack = dateBack;
        }

    }
}
