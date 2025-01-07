using Microsoft.AspNetCore.Identity;
using WebLibrary.Entities;

namespace WebLibrary.Data {
    public class SeedDB {
        private readonly LibraryContext _context;
        private readonly UserManager<User> _manager;

        public SeedDB(LibraryContext context, UserManager<User> manager) {
            _context = context;
            _manager = manager;
        }

        public async Task Seed() {
            User u = new User();
            string email = "dionclei2@gmail.com";
            u.UserName = email;
            u.Email = email;
            u.Name = "Dionclei de Souza";
            if (!_context.Users.Any()) {
                Category c1 = new Category("Horror");
                Category c2 = new Category("Si-fi");
                Category c3 = new Category("Adventure");
                Category c4 = new Category("Drama");
                Category c5 = new Category("Comedy");
                Category c6 = new Category("Thriller");
                Category c7 = new Category("Historical");
                _context.Categories.AddRange(c1, c2, c3, c4, c5, c6, c7);
                var result = await _manager.CreateAsync(u, "ILoveDotNet");
                u = new User();
                email = "pedro13@gmail.com";
                u.UserName = email;
                u.Email = email;
                u.Name = "Pedro Mirok";
                await _manager.CreateAsync(u, "IPlayMusic");
                Book b = new Book("Lorem ipsum dolor", "Dolor Doler", c2);
                _context.Books.Add(b);
                b = new Book("Ea atque autem quo totam", "Quibusdam et", c4);
                _context.Books.Add(b);
                _context.SaveChanges();
                Loan l = new Loan(u, b, DateTime.Now, DateTime.Now.AddDays(7));
                _context.Loans.Add(l);
                _context.SaveChanges();
            }
        }
    }
}
