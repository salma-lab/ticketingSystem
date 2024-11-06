namespace TicketAPI.Models
{
    public class Commentaire
    {
        public int CommentaireId { get; set; }
        public int TicketId { get; set; }
        public string Contenu { get; set; }
        public DateTime DateCommentaire { get; set; } = DateTime.Now;

        public Ticket Ticket { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }

}
