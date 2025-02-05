namespace TicketAPI.Models
{
    public class TicketUpdateDto
    {
        public int TicketId { get; set; }

       
        public int? StatusId { get; set; }
        public bool? Validation1 { get; set; }
        public string ?Description { get; set; }
        public int ? IdIntervenant{ get; set; }
        public DateTime DateStart { get; set; }












        //public DateTime DateValidation { get; set; }

    }
}
