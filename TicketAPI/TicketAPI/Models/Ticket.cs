using System;
using System.Text.Json.Serialization;

namespace TicketAPI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string ?Description { get; set; }

        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime DateCreation { get; set; }


        public int ?StatusId { get; set; }
        public int TypeInterventionId { get; set; }
        public bool Oralement { get; set; }
        public bool ? Validation1 { get; set; } = false;
        public bool? Started { get; set; } = false;


        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime? ValidationTime { get; set; } // Nullable to handle cases where validation hasn't occurred yet
        public DateTime? StartTime { get; set; } // Nullable to handle cases where validation hasn't occurred yet


        public TimeSpan? Duration => ValidationTime.HasValue
        ? ValidationTime.Value - DateCreation
        : null;

        public int? EtageId { get; set; }
        public int EmplacementId { get; set; }
        public string AppareilNom { get; set; }
        public string MotifDemande { get; set; }
        public int TypeAppareilId { get; set; }
        public int UtilisateurId { get; set; }
        public int? IdIntervenant { get; set; }


        public Status Status { get; set; }
        public TypeIntervention TypeIntervention { get; set; }
        public Utilisateur Utilisateur { get; set; } // Navigation vers l'utilisateur
        public TypeAppareil TypeAppareil { get; set; }
        public Intervenant Intervenant { get; set; }
        public Etage Etage { get; set; }
        public Emplacement Emplacement { get; set; }
        public string NomDemandeur => Utilisateur != null ? $"{Utilisateur.Nom} {Utilisateur.Prenom}" : "Inconnu";

    }
}
