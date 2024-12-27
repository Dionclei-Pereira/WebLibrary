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
            if (!_context.Users.Any()) {
                User u = new User();
                string email = "dionclei2@gmail.com";
                u.UserName = email;
                u.Email = email;
                u.Name = "Dionclei de Souza";
                var result = await _manager.CreateAsync(u, "ILoveDotNet");
                u = new User();
                email = "pedro13@gmail.com";
                u.UserName = email;
                u.Email = email;
                u.Name = "Pedro Mirok";
                await _manager.CreateAsync(u, "IPlayMusic");
            }
            if (!_context.Books.Any()) {
                Book b = new Book("Lorem ipsum dolor", "Dolor Doler");
                _context.Books.Add(b);
                b = new Book("Ea atque autem quo totam", "Quibusdam et");
                _context.Books.Add(b);
                _context.SaveChanges();
            }
        }
    }
}
