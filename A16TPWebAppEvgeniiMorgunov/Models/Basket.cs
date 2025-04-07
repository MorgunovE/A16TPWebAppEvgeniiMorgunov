using System.Collections.Generic;

namespace A16TPWebAppEvgeniiMorgunov.Models
{
    public class Basket
    {
        public long Id { get; set; }

        public long? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<BasketLivre>? BasketLivres { get; set; }
    }
}