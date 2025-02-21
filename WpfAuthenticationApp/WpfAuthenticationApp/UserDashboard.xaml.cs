using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfAuthenticationApp.Models;
using Newtonsoft.Json;

using System.Data;

using System.Net.Http.Json;

using System.Threading.Tasks;

using System.Windows.Documents;

using System.Windows.Media;


using System.Globalization;
using ClosedXML.Excel;
using Microsoft.Win32;
using System.Xml;
using System.ComponentModel.DataAnnotations;



namespace WpfAuthenticationApp
{
    public partial class UserDashboard : Window
    {

        private readonly string _token;
        private readonly string _typeInterventionApiUrl = "https://localhost:7046/api/TypesInterventionController";
        private readonly string _statusApiUrl = "https://localhost:7046/api/StatusController";
        private readonly string _ticketApiUrl = "https://localhost:7046/api/TicketsController";
        private readonly string _utilisateurApiUrl = "https://localhost:7046/api/UtilisateursController";
        private readonly string _roleApiUrl = "https://localhost:7046/api/RoleController";
        private readonly string _commentApiUrl = "https://localhost:7046/api/CommentairesController";
        private readonly string baseUrl = "https://localhost:7046"; // Replace with your API base URL
        private readonly string _MyTickets = "https://localhost:7046/api/MyTickets";
  
        private readonly string _typeAppareilApiUrl = "https://localhost:7046/api/TypeAppareilController";
        private readonly string _etageApiUrl = "https://localhost:7046/api/EtageController";
        private readonly string _emplacementApiUrl = "https://localhost:7046/api/EmplacementController";
        private readonly string _intervenantApiUrl = "https://localhost:7046/api/IntervenantController";








        public ObservableCollection<TypeInterventions> TypeInterventionss { get; set; } = new();
        public ObservableCollection<Statusq> Statusess { get; set; } = new();
        public ObservableCollection<Tickets> Ticketss { get; set; } = new();
        public ObservableCollection<Commentaire> Commentaires { get; set; } = new();
       
        public ObservableCollection<Utilisateur> Utilisateurs { get; set; } = new();
        public ObservableCollection<UtilisateurDTO> Utilisateurss { get; set; } = new();
        public ObservableCollection<Role> Roless { get; set; } = new();
      
        public ObservableCollection<Etages> Etagess { get; set; } = new();
        public ObservableCollection<Emplacements> Emplacements { get; set; } = new();
        public ObservableCollection<TypeAppareils> TypeAppareilss { get; set; } = new();
        public ObservableCollection<Intervenant> Intervenantss { get; set; } = new();








        public UserDashboard(string token)
        {
            InitializeComponent();
            _token = token;
            DataContext = this;
            LoadTypeInterventionss();

            LoadStatuses();
           

        }









        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////// ROLE:://////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>


        public class OuterObjectWithValues<T>
        {
            [JsonPropertyName("$values")]
            public List<T> Values { get; set; }
        }


        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////:
        /// TYPEINTERVENTION :::::://////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        #region Type Intervention Methods
        private async void LoadTypeInterventionss()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(_typeInterventionApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var outerObject = System.Text.Json.JsonSerializer.Deserialize<OuterObject<TypeInterventions>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var interventions = outerObject?.Values ?? new List<TypeInterventions>();

                TypeInterventions.Clear();
                foreach (var intervention in interventions)
                {
                    TypeInterventions.Add(intervention);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Text.Json.JsonException ex)
            {
                MessageBox.Show($"Error parsing data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        #endregion
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Status/////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        #region Status Methods
        private async void LoadStatuses()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(_statusApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var outerObject = System.Text.Json.JsonSerializer.Deserialize<OuterObject<Statusq>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var statuses = outerObject?.Values ?? new List<Statusq>();

                Statuses.Clear();
                foreach (var status in statuses)
                {
                    Statuses.Add(status);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Text.Json.JsonException ex)
            {
                MessageBox.Show($"Error parsing data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        #endregion

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////: Ticket//////////////////////////////////////////////////////////////////
        /// </summary>

        private async void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the form data
            var oralement = NewTicketOralementCheckBox.IsChecked ?? false; // Check if Oralement is checked
            var appareilNom = NewTicketAppareilNomTextBox.Text.Trim();
            var etage = NewTicketEtageComboBox.SelectedItem as Etages;
            var emplacement = NewTicketEmplacementComboBox.SelectedItem as Emplacements;
            var motif = NewTicketMotifTextBox.Text.Trim();
            var type = NewTicketTypeComboBox.SelectedItem as TypeIntervention; // Assuming you have TypeInterventions in the ViewModel
            var typeApp = NewTypeAppareilComboBox.SelectedItem as TypeAppareil;
            var nomDe = NewTicketNomDeTextBox.Text.Trim();// Assuming you have TypeInterventions in the ViewModel

            // Get the selected date from the DatePicker
            var selectedDate = NewTicketDatePicker.SelectedDate ?? DateTime.Now;

            // Validate input
            if (string.IsNullOrEmpty(motif) || type == null || etage == null ||  typeApp == null || emplacement == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new Ticket object to send to the API
            var newTicket = new TicketCreateDtos
            {
                Oralement = oralement,
                AppareilNom = appareilNom,
               
                EtageId = etage.EtageId,
                EmplacementId = emplacement.EmplacementId,
                TypeAppareilId = typeApp.TypeAppareilId,
                MotifDemande = motif,
                TypeInterventionId = type.TypeInterventionId,
                DateCreation = selectedDate // Use the selected date
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token); // _token should hold your JWT token
                var json = JsonSerializer.Serialize(newTicket);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Send the POST request to create a new ticket
                var response = await client.PostAsync(_ticketApiUrl, content);
                response.EnsureSuccessStatusCode(); // Will throw an exception if status code is not successful

                // Reload tickets after adding
                

                // Clear the input fields
                NewTicketOralementCheckBox.IsChecked = false;
                NewTicketAppareilNomTextBox.Clear();
                NewTicketEtageComboBox.SelectedIndex = -1;
                NewTicketEmplacementComboBox.SelectedIndex = -1;
                NewTicketMotifTextBox.Clear();
                NewTicketTypeComboBox.SelectedIndex = -1;
                NewTypeAppareilComboBox.SelectedIndex = -1;

                // Reset the DatePicker
                NewTicketDatePicker.SelectedDate = null;

                // Show success message
                MessageBox.Show("Ticket ajouté avec succès !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du ticket: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void LoadEmplacements()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(_emplacementApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var outerObject = JsonSerializer.Deserialize<OuterObject<Emplacements>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var emplacements = outerObject?.Values ?? new List<Emplacements>();

                Emplacements.Clear();
                foreach (var emplacement in emplacements)
                {
                    Emplacements.Add(emplacement);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la récupération des données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de l'analyse des données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





























        // Helper class to represent the JSON structure
        public class OuterObject<T>
        {
            [JsonPropertyName("$values")]
            public List<T> Values { get; set; }
        }

        private void NomTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void NewCommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    // TypeIntervention class
    public class TypeInterventions
    {
        public int TypeInterventionId { get; set; }
        public string NomType { get; set; }
    }

    // Status class
    public class Statusq
    {
        public int StatusId { get; set; }
        public string NomStatus { get; set; }
    }
    // Ticket class
    public class Tickets
    {
        public int TicketId { get; set; }
        public string Email { get; set; }


        public string Description { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;

        public bool Oralement { get; set; }
        public string AppareilNom { get; set; }
        public string Etage { get; set; }
        public string Emplacement { get; set; }
        public string MotifDemande { get; set; }
        public string NomType { get; set; }
        public string NomStatus { get; set; }

    }


    public class TicketCreateDtos
    {
        public string Description { get; set; }
        public bool Oralement { get; set; }
        public string AppareilNom { get; set; }
        public string Etage { get; set; }
        public string Emplacement { get; set; }
        public string MotifDemande { get; set; }
        public int TypeInterventionId { get; set; }
        public int StatusId { get; set; }
        public string NomType { get; set; }

    }




    public class Emplacements
    {

        public int EmplacementId { get; set; }
        public string NomEmplacement { get; set; }

    }
    public class TypeAppareils
    {
        public int TypeAppareilId { get; set; }
        public string NomTypeAppareil { get; set; }
    }
    public class Etages
    {
        public int EtageId { get; set; }
        public string NomEtage { get; set; }
    }

}








