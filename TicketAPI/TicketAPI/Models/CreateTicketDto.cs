namespace TicketAPI.Models
{
    public class CreateTicketDto
    {
        public int TicketId { get; set; }

        public string Description { get; set; }
        public bool Oralement { get; set; }
        public string AppareilNom { get; set; }
        public string Etage { get; set; }
        public string Emplacement { get; set; }
        public string MotifDemande { get; set; }

        public int StatusId { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;

        public int TypeInterventionId { get; set; }
        public int UtilisateurId { get; set; } // Ajout de la propriété UtilisateurId

    }
}
