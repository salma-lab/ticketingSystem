using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAuthenticationApp.Models
{

    // Ticket.cs
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
        public string NomType { get; set; }
        public string NomStatus { get; set; }
    }

}
