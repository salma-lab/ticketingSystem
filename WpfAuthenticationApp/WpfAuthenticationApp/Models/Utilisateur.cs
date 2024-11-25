using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAuthenticationApp.Models
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

        public string PasswordHash { get; set; }
        [Required]

        public int RoleId { get; set; }

        public Role Role { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}
