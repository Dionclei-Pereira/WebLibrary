using WebLibrary.DTO;

namespace WebLibrary.Entities {
    public class Book {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public Book() {}

        public Book(string name, string author) {
            Name = name;
            Author = author;
        }

        public BookDTO ToDto() {
            return new BookDTO(Id, Name, Author);
        }

    }
}
