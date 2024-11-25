using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAuthenticationApp
{
    public partial class AdminDashboard : Window
    {
        private readonly string _token;
        private readonly string _typeInterventionApiUrl = "https://localhost:7046/api/TypesInterventionController";
        private readonly string _statusApiUrl = "https://localhost:7046/api/StatusController";
        private readonly string _ticketApiUrl = "https://localhost:7046/api/TicketsController";



        public ObservableCollection<TypeIntervention> TypeInterventions { get; set; } = new();
        public ObservableCollection<Status> Statuses { get; set; } = new();
        public ObservableCollection<Ticket> Tickets { get; set; } = new();


        public AdminDashboard(string token)
        {
            InitializeComponent();
            _token = token;
            DataContext = this;
            LoadTypeInterventions();
            LoadStatuses();
            LoadTickets();
        }

        #region Type Intervention Methods
        private async void LoadTypeInterventions()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(_typeInterventionApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var outerObject = JsonSerializer.Deserialize<OuterObject<TypeIntervention>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var interventions = outerObject?.Values ?? new List<TypeIntervention>();

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
            catch (JsonException ex)
            {
                MessageBox.Show($"Error parsing data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddTypeIntervention_Click(object sender, RoutedEventArgs e)
        {
            var name = NewTypeNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name for the type intervention.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newIntervention = new TypeIntervention { NomType = name };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var json = JsonSerializer.Serialize(newIntervention);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_typeInterventionApiUrl, content);
                response.EnsureSuccessStatusCode();

                // Reload the interventions after adding
                LoadTypeInterventions();

                // Clear the TextBox after adding the new intervention
                NewTypeNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error adding type intervention: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteTypeIntervention_Click(object sender, RoutedEventArgs e)
        {
            var selectedIntervention = TypeInterventionDataGrid.SelectedItem as TypeIntervention;
            if (selectedIntervention == null)
            {
                MessageBox.Show("Please select an intervention to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.DeleteAsync($"{_typeInterventionApiUrl}/{selectedIntervention.TypeInterventionId}");
                response.EnsureSuccessStatusCode();

                // Reload the interventions after deletion
                LoadTypeInterventions();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error deleting type intervention: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

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
                var outerObject = JsonSerializer.Deserialize<OuterObject<Status>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var statuses = outerObject?.Values ?? new List<Status>();

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
            catch (JsonException ex)
            {
                MessageBox.Show($"Error parsing data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddStatus_Click(object sender, RoutedEventArgs e)
        {
            var name = NewStatusNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name for the status.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newStatus = new Status { NomStatus = name };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var json = JsonSerializer.Serialize(newStatus);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_statusApiUrl, content);
                response.EnsureSuccessStatusCode();

                // Reload the statuses after adding
                LoadStatuses();

                // Clear the TextBox after adding the new status
                NewStatusNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error adding status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteStatus_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = StatusDataGrid.SelectedItem as Status;
            if (selectedStatus == null)
            {
                MessageBox.Show("Please select a status to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.DeleteAsync($"{_statusApiUrl}/{selectedStatus.StatusId}");
                response.EnsureSuccessStatusCode();

                // Reload the statuses after deletion
                LoadStatuses();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error deleting status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion


        private async void LoadTickets()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(_ticketApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var outerObject = JsonSerializer.Deserialize<OuterObject<Ticket>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var tickets = outerObject?.Values ?? new List<Ticket>();

                Tickets.Clear();
                foreach (var ticket in tickets)
                {
                    Tickets.Add(ticket);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error fetching tickets: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error parsing tickets data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            var description = NewTicketDescriptionTextBox.Text.Trim();
            var status = (TicketStatusComboBox.SelectedItem as Status)?.NomStatus;

            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Please enter a description and select a status for the ticket.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newTicket = new Ticket { Description = description };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var json = JsonSerializer.Serialize(newTicket);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_ticketApiUrl, content);
                response.EnsureSuccessStatusCode();

                // Reload the tickets after adding
                LoadTickets();

                // Clear the input fields after adding the new ticket
                NewTicketDescriptionTextBox.Clear();
                TicketStatusComboBox.SelectedIndex = -1;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error adding ticket: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void DeleteTicket_Click(object sender, RoutedEventArgs e)
        {
            var selectedTicket = TicketDataGrid.SelectedItem as Ticket;
            if (selectedTicket == null)
            {
                MessageBox.Show("Please select a ticket to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.DeleteAsync($"{_ticketApiUrl}/{selectedTicket.TicketId}");
                response.EnsureSuccessStatusCode();

                // Reload the tickets after deletion
                LoadTickets();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error deleting ticket: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        // Helper class to represent the JSON structure
        public class OuterObject<T>
        {
            [JsonPropertyName("$values")]
            public List<T> Values { get; set; }
        }
    }

    // TypeIntervention class
    public class TypeIntervention
    {
        public int TypeInterventionId { get; set; }
        public string NomType { get; set; }
    }

    // Status class
    public class Status
    {
        public int StatusId { get; set; }
        public string NomStatus { get; set; }
    }
    // Ticket class
    public class Ticket
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

}
