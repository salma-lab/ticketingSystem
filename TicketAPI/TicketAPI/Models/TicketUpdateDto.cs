namespace TicketAPI.Models
{
    public class TicketUpdateDto
    {
        public int ticketId { get; set; }

        public string ? Description { get; set; }
        public bool ? Oralement { get; set; }
        public string? AppareilNom { get; set; }
        public int ?EtageId { get; set; }
        public int ?EmplacementId { get; set; }
        public DateTime? ValidationTime { get; set; }



        public string? MotifDemande { get; set; }

        public int ?StatusId { get; set; }
        public bool? Validation1 { get; set; }

        public int ?TypeAppareilId { get; set; }
        public string? Email { get; set; }
        public DateTime? DateCreation { get; set; }




        public int? TypeInterventionId { get; set; }
        public int ? UtilisateurId { get; set; }
        //public DateTime DateValidation { get; set; }

    }
}
