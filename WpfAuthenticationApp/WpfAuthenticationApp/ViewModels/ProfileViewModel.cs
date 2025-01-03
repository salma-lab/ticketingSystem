using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfAuthenticationApp.Models;

namespace WpfAuthenticationApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public ObservableCollection<Ticket> Tickets { get; set; }
        public ObservableCollection<Commentaire> Comments { get; set; }

        private string newCommentContent;
        public string NewCommentContent
        {
            get => newCommentContent;
            set
            {
                newCommentContent = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommentCommand { get; }

        public ProfileViewModel()
        {
            Tickets = new ObservableCollection<Ticket>
            {
                new Ticket
                {
                    ticketId = 1,
                    Description = "UI Bug Fix",
                    AppareilNom = "Printer",
                    NomStatus = "Open"
                },
                new Ticket
                {
                    ticketId = 2,
                    Description = "Add Dark Mode",
                    AppareilNom = "Website",
                    NomStatus = "Pending"
                }
            };

            Comments = new ObservableCollection<Commentaire>();

            AddCommentCommand = new RelayCommand(AddComment);
        }

        private void AddComment(object ticketObject)
        {
            if (ticketObject is Ticket ticket && !string.IsNullOrWhiteSpace(NewCommentContent))
            {
                Comments.Add(new Commentaire
                {
                    TicketId = ticket.ticketId,
                    Contenu = NewCommentContent,
                    DateCommentaire = System.DateTime.Now
                });
                NewCommentContent = string.Empty;
            }
        }
    }
}
