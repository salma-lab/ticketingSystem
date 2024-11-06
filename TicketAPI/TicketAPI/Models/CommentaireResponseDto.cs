namespace TicketAPI.Models
{
    public class CommentaireResponseDto
    {
        public int CommentaireId { get; set; }
        public int TicketId { get; set; }
        public string Contenu { get; set; }
        public DateTime DateCommentaire { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

    }
}
