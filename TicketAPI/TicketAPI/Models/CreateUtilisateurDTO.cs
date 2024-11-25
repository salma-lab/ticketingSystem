namespace TicketAPI.Models
{
    public class CreateUtilisateurDTO
    {
        public int UtilisateurId { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public int RoleId { get; set; } 
    }
}
