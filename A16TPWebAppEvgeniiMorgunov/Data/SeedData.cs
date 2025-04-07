using A16TPWebAppEvgeniiMorgunov.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace A16TPWebAppEvgeniiMorgunov.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // Vérifier si la base de données contient déjà des données
            if (context.Users.Any() || context.Livres.Any())
            {
                return; // La base de données a déjà été initialisée
            }

            // Ajouter des utilisateurs
            var users = new User[]
            {
                new User {
                    Name = "John",
                    FamilyName = "Doe",
                    Tel = "123-456-7890",
                    Email = "john.doe@example.com"
                },
                new User {
                    Name = "Jane",
                    FamilyName = "Smith",
                    Tel = "098-765-4321",
                    Email = "jane.smith@example.com"
                },
                new User {
                    Name = "Alice",
                    FamilyName = "Johnson",
                    Tel = "555-123-4567",
                    Email = "alice.johnson@example.com"
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // Ajouter des livres
            var livres = new Livre[]
            {
                new Livre {
                    Title = "The Great Gatsby",
                    Description = "A novel set in the Roaring Twenties",
                    Author = "F. Scott Fitzgerald",
                    Genre = "Fiction",
                    Image = "great_gatsby.jpg",
                    Price = 10.99,
                    Quantity = 5
                },
                new Livre {
                    Title = "To Kill a Mockingbird",
                    Description = "A novel about racial injustice",
                    Author = "Harper Lee",
                    Genre = "Fiction",
                    Image = "to_kill_a_mockingbird.jpg",
                    Price = 8.99,
                    Quantity = 3
                },
                new Livre {
                    Title = "1984",
                    Description = "A dystopian novel",
                    Author = "George Orwell",
                    Genre = "Science Fiction",
                    Image = "1984.jpg",
                    Price = 12.99,
                    Quantity = 7
                }
            };
            context.Livres.AddRange(livres);
            context.SaveChanges();

            // Créer des paniers pour les utilisateurs
            var baskets = new Basket[]
            {
                new Basket { UserId = 1 },
                new Basket { UserId = 2 },
                new Basket { UserId = 3 }
            };
            context.Baskets.AddRange(baskets);
            context.SaveChanges();

            // Ajouter des livres aux paniers
            var basketLivres = new BasketLivre[]
            {
                new BasketLivre { BasketId = 1, LivreId = 1 },
                new BasketLivre { BasketId = 1, LivreId = 2 },
                new BasketLivre { BasketId = 2, LivreId = 3 },
                new BasketLivre { BasketId = 3, LivreId = 1 },
                new BasketLivre { BasketId = 3, LivreId = 3 }
            };
            context.BasketLivres.AddRange(basketLivres);
            context.SaveChanges();
        }
    }
}