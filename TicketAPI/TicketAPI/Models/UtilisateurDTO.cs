namespace TicketAPI.Models
{
    public class UtilisateurDTO
    {
        public int UtilisateurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; } 

        public List<TicketDTO> Tickets { get; set; } = new List<TicketDTO>();


    }
}
