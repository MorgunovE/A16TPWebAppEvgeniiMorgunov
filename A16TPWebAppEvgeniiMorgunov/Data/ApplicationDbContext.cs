using A16TPWebAppEvgeniiMorgunov.Models;
using Microsoft.EntityFrameworkCore;

namespace A16TPWebAppEvgeniiMorgunov.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Livre> Livres { get; set; } = null!;
        public DbSet<Basket> Baskets { get; set; } = null!;
        public DbSet<BasketLivre> BasketLivres { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketLivre>()
                .HasKey(bl => new { bl.BasketId, bl.LivreId });

            modelBuilder.Entity<BasketLivre>()
                .HasOne(bl => bl.Basket)
                .WithMany(b => b.BasketLivres)
                .HasForeignKey(bl => bl.BasketId);

            modelBuilder.Entity<BasketLivre>()
                .HasOne(bl => bl.Livre)
                .WithMany()
                .HasForeignKey(bl => bl.LivreId);

            modelBuilder.Entity<Basket>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}