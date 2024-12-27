namespace WebLibrary.DTO {
    public class BookDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public BookDTO() { }

        public BookDTO(int id, string name, string author) {
            Id = id;
            Name = name;
            Author = author;
        }

    }
}
