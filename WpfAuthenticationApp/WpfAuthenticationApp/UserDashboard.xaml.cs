using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfAuthenticationApp.Models;
using WpfAuthenticationApp.Views;
using Newtonsoft.Json;



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







        public ObservableCollection<TypeInterventions> TypeInterventions { get; set; } = new();
        public ObservableCollection<Statusq> Statuses { get; set; } = new();
        public ObservableCollection<Ticket> Tickets { get; set; } = new();
        public ObservableCollection<Commentaire> Commentaire { get; set; } = new();








        public UserDashboard(string token)
        {
            InitializeComponent();
            _token = token;
            DataContext = this;
            LoadTypeInterventionss();
            LoadStatuses();
            LoadMyTicketsAsync();

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
            // Retrieve form data
            var description = NewTicketDescriptionTextBox.Text.Trim();
            var oralement = NewTicketOralementCheckBox.IsChecked ?? false; // If checkbox is checked
            var appareilNom = NewTicketAppareilNomTextBox.Text.Trim();
            var etage = NewTicketEtageTextBox.Text.Trim();
            var emplacement = NewTicketEmplacementTextBox.Text.Trim();
            var motif = NewTicketMotifTextBox.Text.Trim();
            var type = NewTicketTypeComboBox.SelectedItem as TypeInterventions; // Selected item from ComboBox

            // Check if type is selected
            if (type == null)
            {
                MessageBox.Show("Please select a type of intervention.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate if required fields are filled
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(appareilNom) || string.IsNullOrEmpty(etage) || string.IsNullOrEmpty(emplacement) || string.IsNullOrEmpty(motif))
            {
                MessageBox.Show("Please fill all the required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new Ticket object for the API
            var newTicket = new TicketCreateDto
            {
                Description = description,
                Oralement = oralement,
                AppareilNom = appareilNom,
                Etage = etage,
                Emplacement = emplacement,
                MotifDemande = motif,
                TypeInterventionId = type.TypeInterventionId, // Ensure TypeInterventionId is being used
                StatusId = 5 // Set to "En cours" by default
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token); // Ensure token is valid
                var json = System.Text.Json.JsonSerializer.Serialize(newTicket);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Send the request to create the ticket
                var response = await client.PostAsync(_ticketApiUrl, content);
                response.EnsureSuccessStatusCode(); // Will throw an exception if status is not success

                // Reload ticket list or refresh as necessary

                // Clear form fields after adding ticket
                NewTicketDescriptionTextBox.Clear();
                NewTicketOralementCheckBox.IsChecked = false;
                NewTicketAppareilNomTextBox.Clear();
                NewTicketEtageTextBox.Clear();
                NewTicketEmplacementTextBox.Clear();
                NewTicketMotifTextBox.Clear();
                NewTicketTypeComboBox.SelectedIndex = -1; // Reset ComboBox selection
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error adding ticket: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private async Task LoadMyTicketsAsync()
        {
            try
            {
                // Set up the HttpClient
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token); // Ensure valid JWT token
                client.BaseAddress = new Uri("https://localhost:7046");

                // Send the GET request to the MyTickets endpoint
                var response = await client.GetAsync("MyTickets");

                // Check the response status
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response to a list of tickets
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var tickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonResponse);

                    // Bind the data to a UI element (e.g., DataGrid)
                    TicketsDataGrid.ItemsSource = tickets;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("You are not authorized. Please check your login credentials or token.",
                                    "Unauthorized",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"Error fetching tickets: {response.ReasonPhrase} ({(int)response.StatusCode})",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error connecting to the server: {ex.Message}",
                                "Connection Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
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
}








