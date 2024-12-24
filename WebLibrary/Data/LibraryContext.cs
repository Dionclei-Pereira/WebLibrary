using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebLibrary.Data {
    public class LibraryContext : IdentityDbContext {

        public LibraryContext(DbContextOptions options) : base (options) {

        }

    }
}
