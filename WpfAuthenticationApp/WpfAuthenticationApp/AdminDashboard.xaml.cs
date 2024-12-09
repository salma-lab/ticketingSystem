using System.Collections.ObjectModel;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAuthenticationApp.Models;


namespace WpfAuthenticationApp
{
    public partial class AdminDashboard : Window
    {
        private readonly string _token;
        private readonly string _typeInterventionApiUrl = "https://localhost:7046/api/TypesInterventionController";
        private readonly string _statusApiUrl = "https://localhost:7046/api/StatusController";
        private readonly string _ticketApiUrl = "https://localhost:7046/api/TicketsController";
        private readonly string _utilisateurApiUrl = "https://localhost:7046/api/UtilisateursController";
        private readonly string _roleApiUrl = "https://localhost:7046/api/RoleController";
        private readonly string _commentApiUrl = "https://localhost:7046/api/CommentairesController";




        public ObservableCollection<TypeIntervention> TypeInterventions { get; set; } = new();
        public ObservableCollection<Status> Statuses { get; set; } = new();
        public ObservableCollection<Ticket> Tickets { get; set; } = new();
        public ObservableCollection<Utilisateur> Utilisateurs { get; set; } = new();
        public ObservableCollection<UtilisateurDTO> Utilisateurss { get; set; } = new();
        public ObservableCollection<Role> Roles { get; set; } = new();
        public ObservableCollection<Commentaire> Commentaire { get; set; } = new();






        public AdminDashboard(string token)
        {
            InitializeComponent();
            _token = token;
            DataContext = this;
            LoadTypeInterventions();
            LoadStatuses();
            LoadTickets();
            LoadUtilisateurs();
            LoadRoles();
           
        }
/// <summary>
/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// //////////////////////// ROLE:://////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        private async void LoadRoles()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(_roleApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                // Parse the JSON response
                var outerObject = JsonSerializer.Deserialize<OuterObjectWithValues<Role>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var roles = outerObject?.Values ?? new List<Role>();

                Roles.Clear();
                foreach (var role in roles)
                {
                    Roles.Add(role);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error fetching roles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error parsing roles data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


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

/// <summary>
/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// /////////////////////////////////////: Ticket//////////////////////////////////////////////////////////////////
/// </summary>


        private int SelectedTicketId = 0;


        private void TicketDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected ticket from the DataGrid
            var selectedTicket = TicketDataGrid.SelectedItem as Ticket;

            if (selectedTicket != null)
            {
                // Store the selected ticket's ID
                SelectedTicketId = selectedTicket.TicketId; // Assuming TicketId is a property in the Ticket model
            }
            else
            {
                // Handle the case when no ticket is selected
                SelectedTicketId = 0;
            }
        }

        //private void TicketList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
          //  var selectedTicket = TicketList.SelectedItem as Ticket; // Replace with your data type
           // if (selectedTicket != null)
            //{
               // Console.WriteLine($"Selected TicketId: {selectedTicket.TicketId}");
               // await LoadTicketComments(selectedTicket.TicketId);
           // }
       // }



        private async void AddComment_Click(object sender, RoutedEventArgs e)
        {
            // Ensure a ticket is selected
            if (SelectedTicketId == 0)
            {
                MessageBox.Show("Please select a ticket to add a comment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Get the content of the new comment
            var commentContent = NewCommentTextBox.Text.Trim();

            if (string.IsNullOrEmpty(commentContent))
            {
                MessageBox.Show("Please enter a comment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var commentaireDto = new CommentaireDto
            {
                TicketId = SelectedTicketId,
                Contenu = commentContent
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var json = JsonSerializer.Serialize(commentaireDto);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_commentApiUrl, content);
                response.EnsureSuccessStatusCode();

                // Clear the comment input field
                NewCommentTextBox.Clear();

                // Optionally, you can reload or refresh the ticket's comments list
                LoadTicketComments(SelectedTicketId);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error adding comment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private async void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the form data
            var description = NewTicketDescriptionTextBox.Text.Trim();
            var oralement = NewTicketOralementCheckBox.IsChecked ?? false; // Check if Oralement is checked
            var appareilNom = NewTicketAppareilNomTextBox.Text.Trim();
            var etage = NewTicketEtageTextBox.Text.Trim();
            var emplacement = NewTicketEmplacementTextBox.Text.Trim();
            var motif = NewTicketMotifTextBox.Text.Trim();
            var type = NewTicketTypeComboBox.SelectedItem as TypeIntervention; // Assuming you have TypeInterventions in the ViewModel
            var status = TicketStatusComboBox.SelectedItem as Status;

            // Validate input
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(appareilNom) || string.IsNullOrEmpty(etage) || string.IsNullOrEmpty(emplacement) || string.IsNullOrEmpty(motif) || type == null || status == null)
            {
                MessageBox.Show("Please fill all the required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new Ticket object to send to the API
            var newTicket = new TicketCreateDto
            {
                Description = description,
                Oralement = oralement,
                AppareilNom = appareilNom,
                Etage = etage,
                Emplacement = emplacement,
                MotifDemande = motif,
                TypeInterventionId = type.TypeInterventionId, // Assuming TypeIntervention has an Id
                StatusId = status.StatusId // Assuming Status has an Id
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
                LoadTickets();

                // Clear the input fields
                NewTicketDescriptionTextBox.Clear();
                NewTicketOralementCheckBox.IsChecked = false;
                NewTicketAppareilNomTextBox.Clear();
                NewTicketEtageTextBox.Clear();
                NewTicketEmplacementTextBox.Clear();
                NewTicketMotifTextBox.Clear();
                NewTicketTypeComboBox.SelectedIndex = -1;
                TicketStatusComboBox.SelectedIndex = -1;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error adding ticket: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async Task LoadTicketComments(int ticketId)
        {
            // Ensure ticketId is valid
            if (ticketId <= 0)
            {
                MessageBox.Show("ID de ticket invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Set up the HttpClient with the Bearer token
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                // Make the API call to retrieve comments for the specific ticket
                var response = await client.GetAsync($"https://localhost:7046/api/CommentairesController/{ticketId}");

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the response content
                    var commentsJson = await response.Content.ReadAsStringAsync();
                    var comments = JsonSerializer.Deserialize<List<CommentaireResponseDto>>(commentsJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Ensure case-insensitive matching of JSON properties
                    });

                    // Bind the deserialized comments to the UI (e.g., a DataGrid)
                    TicketCommentsDataGrid.ItemsSource = comments ?? new List<CommentaireResponseDto>();
                }
                else
                {
                    // Display an error message with the status code and reason
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erreur lors du chargement des commentaires : {response.StatusCode} - {response.ReasonPhrase}\n{errorMessage}",
                                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle potential network errors
                MessageBox.Show($"Erreur de connexion : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show($"Une erreur inattendue est survenue : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


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






        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////:///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Utilisateur////////////////////////////////////////////////////////////////////////////:::
        /// </summary>

        private async void LoadUtilisateurs()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(_utilisateurApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var outerObject = JsonSerializer.Deserialize<OuterObject<UtilisateurDTO>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var utilisateurss = outerObject?.Values ?? new List<UtilisateurDTO>();

                Utilisateurss.Clear();
                foreach (var utilisateur in utilisateurss)
                {
                    Utilisateurss.Add(utilisateur);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error fetching utilisateurs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error parsing utilisateurs data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method to create a new Utilisateur
        private async void CreateUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            var nom = NomTextBox.Text.Trim();
            var prenom = PrenomTextBox.Text.Trim();
            var email = EmailTextBox.Text.Trim();
            var password = PasswordBox.Password.Trim();
            var selectedRole = RoleComboBox.SelectedItem as Role;

            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || selectedRole == null)
            {
                MessageBox.Show("All fields are required, including selecting a role.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new
            {
                Nom = nom,
                Prenom = prenom,
                Email = email,
                Password = password,
                RoleId = selectedRole.RoleId
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var json = JsonSerializer.Serialize(newUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_utilisateurApiUrl, content);
                response.EnsureSuccessStatusCode();

                MessageBox.Show("Utilisateur created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear the input fields after successful creation
                NomTextBox.Clear();
                PrenomTextBox.Clear();
                EmailTextBox.Clear();
                PasswordBox.Clear();
                RoleComboBox.SelectedIndex = -1;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error creating utilisateur: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error processing utilisateur creation response: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

/// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////:///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Utilisateur////////////////////////////////////////////////////////////////////////////:::
// </summary>



    


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
    

    public class TicketCreateDto
    {
        public string Description { get; set; }
        public bool Oralement { get; set; }
        public string AppareilNom { get; set; }
        public string Etage { get; set; }
        public string Emplacement { get; set; }
        public string MotifDemande { get; set; }
        public int TypeInterventionId { get; set; }
        public int StatusId { get; set; }
    }

    public class Utilisateur
    {
        public int UtilisateurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }






    }
    public class UtilisateurDTO
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }


    }
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } // e.g., "Admin", "Worker", "Technician" yeyye
        public ICollection<Utilisateur> Utilisateurs { get; set; }  // Navigation property for users

    }

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

    public class CommentaireResponseDto
    {
        public int CommentaireId { get; set; }
        public int TicketId { get; set; }
        public string Contenu { get; set; }
        public DateTime DateCommentaire { get; set; }
        public int UtilisateurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }

    public class CommentaireDto
    {
        public int CommentaireId { get; set; }
        public int TicketId { get; set; }
        public string Contenu { get; set; }
        public DateTime DateCommentaire { get; set; }
        public int UtilisateurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }


}
