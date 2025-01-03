namespace TicketAPI.Models
{
    public class TicketDTO
    {
        public int TicketId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public string NomType { get; set; }
        public bool Oralement { get; set; }
        public bool Validation1 { get; set; }
        public TimeSpan? ValidationDuration { get; set; } // Add this property
        public DateTime? ValidationTime { get; set; }

        public string NomEmplacement { get; set; }
        public string NomEtage { get; set; }





        public string AppareilNom { get; set; }

        public string MotifDemande { get; set; }


        public string NomStatus { get; set; } 
        public string NomTypeAppareil {  get; set; }
        public int UtilisateurId { get; set; }

        public string Email { get; set; }


    }
}
