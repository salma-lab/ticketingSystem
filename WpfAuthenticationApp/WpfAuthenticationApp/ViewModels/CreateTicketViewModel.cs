using System;
using System.Windows.Input;
using WpfAuthenticationApp.Models;

namespace WpfAuthenticationApp.ViewModels
{
    public class CreateTicketViewModel : BaseViewModel
    {
        private string description;
        private string appareilNom;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public string AppareilNom
        {
            get => appareilNom;
            set
            {
                appareilNom = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateTicketCommand { get; }

        public CreateTicketViewModel()
        {
            CreateTicketCommand = new RelayCommand(CreateTicket);
        }

        private void CreateTicket(object obj)
        {
            var newTicket = new Ticket
            {
                ticketId = new Random().Next(1000),
                Description = Description,
                AppareilNom = AppareilNom,
                DateCreation = DateTime.Now,
                NomStatus = "New"
            };

            // You can add logic here to save the ticket to a database or a collection.

            // Reset fields
            Description = string.Empty;
            AppareilNom = string.Empty;
        }
    }
}
