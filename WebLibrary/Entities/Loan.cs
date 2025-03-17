using System.ComponentModel.DataAnnotations.Schema;
using WebLibrary.Entities.DTO;

namespace WebLibrary.Entities {
    public class Loan {
        public int Id { get; set; }
        public User User { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public DateTime DateInit { get; set; }
        public DateTime DateBack { get; set; }

        public Loan() { }
        public Loan(User user, Book book, DateTime dateInit, DateTime dateBack) {
            User = user;
            Book = book;
            DateInit = dateInit;
            DateBack = dateBack;
        }

        public LoanDTO ToDTO() {
            return new LoanDTO(Id, User.ToDTO(), Book.ToDto(), DateInit, DateBack);
        }
        public LoanDTOWithoutUser ToDTOWithoutUser() {
            return new LoanDTOWithoutUser(Book.ToDto(), DateInit, DateBack);
        }
    }
}
