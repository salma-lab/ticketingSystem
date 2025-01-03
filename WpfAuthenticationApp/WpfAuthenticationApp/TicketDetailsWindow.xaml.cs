using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfAuthenticationApp
{
    public partial class TicketDetailsWindow : Window
    {
        public ObservableCollection<Status> Statuses { get; set; }
        public Ticket SelectedTicket { get; set; }

        private readonly string _ticketApiUrl = "https://localhost:7046/api/TicketsController";

        public TicketDetailsWindow(Ticket ticket, ObservableCollection<Status> statuses)
        {
            InitializeComponent();
            SelectedTicket = ticket;
            Statuses = statuses;
            DataContext = this;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7046/api/TicketsController");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ticketJson = JsonSerializer.Serialize(SelectedTicket, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                var content = new StringContent(ticketJson, System.Text.Encoding.UTF8, "application/json");

                // Ensure the correct HTTP method (PATCH)
                var response = await client.PatchAsync($"/{SelectedTicket.ticketId}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Le statut a été mis à jour avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    string statusCode = response.StatusCode.ToString();
                    string errorMessage = await response.Content.ReadAsStringAsync();

                    // Provide detailed feedback
                    MessageBox.Show(
                        $"Échec de la mise à jour du statut.\nStatut HTTP : {statusCode}\nErreur API : {errorMessage}",
                        "Erreur",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument doc = new FlowDocument
                {
                    FontFamily = new System.Windows.Media.FontFamily("Arial"),
                    FontSize = 12,
                    PagePadding = new Thickness(20),
                };

                doc.Blocks.Add(new Paragraph(new Run("Ticket Details"))
                {
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center
                });

                doc.Blocks.Add(new Paragraph(new Run($"Email: {SelectedTicket.Email}")));
                doc.Blocks.Add(new Paragraph(new Run($"Nom Etage: {SelectedTicket.NomEtage}")));
                doc.Blocks.Add(new Paragraph(new Run($"Description: {SelectedTicket.Description}")));
                doc.Blocks.Add(new Paragraph(new Run($"Motif Demande: {SelectedTicket.MotifDemande}")));
                doc.Blocks.Add(new Paragraph(new Run($"Status: {SelectedTicket.NomStatus}")));
                doc.Blocks.Add(new Paragraph(new Run($"Oralement: {SelectedTicket.Oralement}")));
                doc.Blocks.Add(new Paragraph(new Run($"Appareil Nom: {SelectedTicket.AppareilNom}")));
                doc.Blocks.Add(new Paragraph(new Run($"Type Appareil: {SelectedTicket.NomTypeAppareil}")));
                doc.Blocks.Add(new Paragraph(new Run($"Emplacement: {SelectedTicket.NomEmplacement}")));
                doc.Blocks.Add(new Paragraph(new Run($"Type Intervention: {SelectedTicket.NomType}")));
                doc.Blocks.Add(new Paragraph(new Run($"Validation Time: {SelectedTicket.ValidationTime:yyyy/MM/dd HH:mm}")));
                doc.Blocks.Add(new Paragraph(new Run($"Date Creation: {SelectedTicket.DateCreation:yyyy/MM/dd HH:mm}")));

                IDocumentPaginatorSource idocument = doc;
                printDialog.PrintDocument(idocument.DocumentPaginator, "Ticket Details");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
