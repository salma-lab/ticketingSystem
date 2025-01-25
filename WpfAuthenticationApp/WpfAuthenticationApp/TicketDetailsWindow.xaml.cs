using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;

namespace WpfAuthenticationApp
{
    public partial class TicketDetailsWindow : Window
    {
        public Ticket SelectedTicket { get; set; }

        private readonly string _ticketApiUrl = "https://localhost:7046/api/TicketsController";

        public TicketDetailsWindow(Ticket ticket)
        {
            InitializeComponent();

            SelectedTicket = ticket;
            DataContext = SelectedTicket;  // Bind the SelectedTicket to the DataContext of the window
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the window
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(_ticketApiUrl);
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
    }
}
