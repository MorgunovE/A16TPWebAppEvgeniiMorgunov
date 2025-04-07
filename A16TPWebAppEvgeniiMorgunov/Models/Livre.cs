using System.ComponentModel.DataAnnotations;

namespace A16TPWebAppEvgeniiMorgunov.Models
{
    public class Livre
    {
        public long Id { get; set; }

        [Display(Name = "Titre")]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Auteur")]
        public string? Author { get; set; }

        [Display(Name = "Genre")]
        public string? Genre { get; set; }

        [Display(Name = "Image")]
        public string? Image { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix doit être positif")]
        [Display(Name = "Prix")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La quantité doit être positive")]
        [Display(Name = "Quantité")]
        public int Quantity { get; set; }
    }
}