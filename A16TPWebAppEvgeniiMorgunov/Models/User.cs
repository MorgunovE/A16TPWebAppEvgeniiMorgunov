using System.ComponentModel.DataAnnotations;

namespace A16TPWebAppEvgeniiMorgunov.Models
{
    public class User
    {
        public long Id { get; set; }

        [Display(Name = "Prénom")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Le nom de famille est obligatoire")]
        [Display(Name = "Nom de famille")]
        public string FamilyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire")]
        [Display(Name = "Téléphone")]
        public string Tel { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
    }
}