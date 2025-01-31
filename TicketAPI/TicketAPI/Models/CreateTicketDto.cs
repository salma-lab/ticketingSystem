namespace TicketAPI.Models
{
    public class CreateTicketDto
    {
        public int TicketId { get; set; }

        public string? Description { get; set; }
        public bool Oralement { get; set; }
        public string AppareilNom { get; set; }
        public int ? EtageId { get; set; }
        public int EmplacementId { get; set; }
        




        public string MotifDemande { get; set; }

        public int StatusId { get; set; }

        public int TypeAppareilId { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public int TypeInterventionId { get; set; }
        public int UtilisateurId { get; set; } // Ajout de la propriété UtilisateurId

    }
}
