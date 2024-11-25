namespace TicketAPI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public int StatusId { get; set; }
        public int TypeInterventionId { get; set; }
        public bool Oralement { get; set; }
        public string AppareilNom { get; set; }
        public string Etage { get; set; }
        public string Emplacement { get; set; }
        public string MotifDemande { get; set; }



        public int UtilisateurId { get; set; }


        public Status Status { get; set; }

        public TypeIntervention TypeIntervention { get; set; }
        public Utilisateur Utilisateur { get; set; } // Navigation vers l'utilisateur

        public ICollection<Commentaire> Commentaires { get; set; }
    }
}
