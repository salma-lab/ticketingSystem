namespace TicketAPI.Models
{
    public class UtilisateurResponseDTO
    {
        public int UtilisateurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
