using System.ComponentModel.DataAnnotations.Schema;
using WebLibrary.Entities.DTO;

namespace WebLibrary.Entities {
    public class Book {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public Category BookCategory { get; set; }

        [InverseProperty("Book")]
        public Loan Loan { get; set; }

        public Book() {}

        public Book(string name, string author, Category category) {
            Name = name;
            Author = author;
            BookCategory = category;
        }

        public BookDTO ToDto() {
            return new BookDTO(Id, Name, Author, BookCategory);
        }
    }
}
