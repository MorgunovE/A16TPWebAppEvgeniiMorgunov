namespace A16TPWebAppEvgeniiMorgunov.Models
{
    public class BasketLivre
    {
        public long BasketId { get; set; }
        public Basket? Basket { get; set; }

        public long LivreId { get; set; }
        public Livre? Livre { get; set; }

        public int Quantity { get; set; } = 1;
    }
}