using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Models
{
    public class Utilisateur
    {
        public int UtilisateurId { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]

        public string Prenom { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string PasswordHash { get; set; } // Hash of the password
        [Required]

        public int RoleId { get; set; }

        public Role Role { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }

}
