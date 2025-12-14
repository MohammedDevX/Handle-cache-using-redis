using Microsoft.EntityFrameworkCore;
using Products_service.Models;

namespace Products_service.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base (options) {}

        public DbSet<Produit> Produit { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
