using System.Text.Json.Serialization;

namespace TicketAPI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime DateCreation { get; set; }
        public int StatusId { get; set; }
        public int TypeInterventionId { get; set; }
        public bool Oralement { get; set; }
        public bool Validation1 { get; set; }
        public bool Validation2 { get; set; }
        public int EtageId { get; set; }
        public int EmplacementId { get; set; }





        public string AppareilNom { get; set; }
        public string MotifDemande { get; set; }
        public int TypeAppareilId { get; set; }



        public int UtilisateurId { get; set; }


        public Status Status { get; set; }

        public TypeIntervention TypeIntervention { get; set; }
        public Utilisateur Utilisateur { get; set; } // Navigation vers l'utilisateur
        public TypeAppareil TypeAppareil { get; set; }
        public Etage Etage { get; set; }
        public Emplacement Emplacement { get; set; }
            

    }
}
