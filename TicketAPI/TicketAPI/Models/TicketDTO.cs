namespace TicketAPI.Models
{
    public class TicketDTO
    {
        public int TicketId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public int StatusId { get; set; }
        public int TypeInterventionId { get; set; }
        public int UtilisateurId { get; set; }
    }
}
