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
            // Configuration de la clé primaire composite pour BasketLivre
            modelBuilder.Entity<BasketLivre>()
                .HasKey(bl => new { bl.BasketId, bl.LivreId });

            // Configuration de la relation entre Basket et BasketLivre
            modelBuilder.Entity<BasketLivre>()
                .HasOne(bl => bl.Basket)
                .WithMany(b => b.BasketLivres)
                .HasForeignKey(bl => bl.BasketId);

            // Configuration de la relation entre Livre et BasketLivre
            modelBuilder.Entity<BasketLivre>()
                .HasOne(bl => bl.Livre)
                .WithMany()
                .HasForeignKey(bl => bl.LivreId);

            // Configuration de la relation entre User et Basket
            modelBuilder.Entity<Basket>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId);

            // Configuration pour assurer l'unicité de l'email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}