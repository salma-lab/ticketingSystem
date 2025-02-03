using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Models
{
    public class Intervenant
    {
        [Key]

        public int IdIntervenant { get; set; }
        public string NomIntervenant { get; set; }
    }
}
