using backend.Models;
using Microsoft.EntityFrameworkCore;


namespace backend.Data {
    public class Context : DbContext {
        public Context(DbContextOptions options) : base(options) {
        }

        public DbSet<Item> Items { get; set; }
    }
}
