using System.Collections.ObjectModel;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WpfAuthenticationApp.Models;
using System.Windows.Media;


using System.Globalization;
using ClosedXML.Excel;
using Microsoft.Win32;
using System.Xml;



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
        private readonly string _typeAppareilApiUrl = "https://localhost:7046/api/TypeAppareilController";
        private readonly string _etageApiUrl = "https://localhost:7046/api/EtageController";
        private readonly string _emplacementApiUrl = "https://localhost:7046/api/EmplacementController";









        public ObservableCollection<TypeIntervention> TypeInterventions { get; set; } = new();
        public ObservableCollection<Status> Statuses { get; set; } = new();
        public ObservableCollection<Ticket> Tickets { get; set; } = new();
        public ObservableCollection<Utilisateur> Utilisateurs { get; set; } = new();
        public ObservableCollection<UtilisateurDTO> Utilisateurss { get; set; } = new();
        public ObservableCollection<Role> Roles { get; set; } = new();
        public ObservableCollection<Commentaire> Commentaire { get; set; } = new();
        public ObservableCollection<Etage> Etages { get; set; } = new();
        public ObservableCollection<Emplacement> Emplacement { get; set; } = new();
        public ObservableCollection<TypeAppareil> TypeAppareils { get; set; } = new();









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
            LoadTypeAppareil();
            LoadEtages();
            LoadEmplacements();


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
                MessageBox.Show($"Erreur lors de la récupération des rôles : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de l'analyse des données des rôles : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Show confirmation dialog
            var result = MessageBox.Show("Voulez-vous vraiment vous déconnecter ?", "Déconnexion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                
                this.Close();

                
                var loginWindow = new LoginWindow();  
                loginWindow.Show();
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





        private async void LoadTypeAppareil()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(_typeAppareilApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var outerObject = JsonSerializer.Deserialize<OuterObject<TypeAppareil>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var typeAppareils = outerObject?.Values ?? new List<TypeAppareil>();

                TypeAppareils.Clear();
                foreach (var typeAppareil in typeAppareils)
                {
                    TypeAppareils.Add(typeAppareil);
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

        private async void AddTypeAppareil_Click(object sender, RoutedEventArgs e)
        {
            var name = NewTypeAppNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Veuillez entrer un nom pour le type d'appareil.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newTypeAppareil = new TypeAppareil { NomTypeAppareil = name };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var json = JsonSerializer.Serialize(newTypeAppareil);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_typeAppareilApiUrl, content);
                response.EnsureSuccessStatusCode();

                // Reload the interventions after adding
                LoadTypeAppareil();

                // Clear the TextBox after adding the new intervention
                NewTypeNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du type d'intervention : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteTypeAppareil_Click(object sender, RoutedEventArgs e)
        {
            var selectedAppareil = TypeAppareilDataGrid.SelectedItem as TypeAppareil;
            if (selectedAppareil == null)
            {
                MessageBox.Show("Veuillez sélectionner une intervention à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.DeleteAsync($"{_typeAppareilApiUrl}/{selectedAppareil.TypeAppareilId}");
                response.EnsureSuccessStatusCode();

                // Reload the interventions after deletion
                LoadTypeAppareil();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du TypeAppareil : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



       

       




        private async void LoadEtages()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(_etageApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var outerObject = JsonSerializer.Deserialize<OuterObject<Etage>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var etages = outerObject?.Values ?? new List<Etage>();

                Etages.Clear();
                foreach (var etage in etages)
                {
                    Etages.Add(etage);
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

        private async void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = StatusDataGrid.SelectedItem as Status;
            if (selectedStatus == null)
            {
                MessageBox.Show("Veuillez sélectionner un statut à mettre à jour.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewStatusNameTextBox.Text))
            {
                MessageBox.Show("Veuillez entrer un nouveau nom de statut.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var updatedStatus = new Status
            {
                NomStatus = NewStatusNameTextBox.Text
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token); // Ensure _token is set
                var json = JsonSerializer.Serialize(updatedStatus);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{_statusApiUrl}/{selectedStatus.StatusId}", content);
                response.EnsureSuccessStatusCode();

                MessageBox.Show("Statut mis à jour avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                 LoadStatuses(); // Reload statuses after update

                NewStatusNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour du statut : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de la sérialisation des données du statut : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateEtage_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = EtageDataGrid.SelectedItem as Etage;
            if (selectedStatus == null)
            {
                MessageBox.Show("Veuillez sélectionner un Etage à mettre à jour.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewEtageNameTextBox.Text))
            {
                MessageBox.Show("Veuillez entrer un nouveau nom d'etage.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var updatedStatus = new Etage
            {
                NomEtage = NewEtageNameTextBox.Text
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token); // Ensure _token is set
                var json = JsonSerializer.Serialize(updatedStatus);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{_etageApiUrl}/{selectedStatus.EtageId}", content);
                response.EnsureSuccessStatusCode();

                MessageBox.Show("Statut mis à jour avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadEtages(); // Reload statuses after update

                NewEtageNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour d'etage : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de la sérialisation des données d'etage : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void UpdateTypeA_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = TypeAppareilDataGrid.SelectedItem as TypeAppareil;
            if (selectedStatus == null)
            {
                MessageBox.Show("Veuillez sélectionner un type d'appareil à mettre à jour.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewTypeAppNameTextBox.Text))
            {
                MessageBox.Show("Veuillez entrer un nouveau nom de type d'appareil.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var updatedStatus = new TypeAppareil
            {
                NomTypeAppareil = NewTypeAppNameTextBox.Text
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token); // Ensure _token is set
                var json = JsonSerializer.Serialize(updatedStatus);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{_typeAppareilApiUrl}/{selectedStatus.TypeAppareilId}", content);
                response.EnsureSuccessStatusCode();

                MessageBox.Show("Type d'appareil mis à jour avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadTypeAppareil(); // Reload statuses after update

                NewTypeAppNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour du type d'appareil : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de la sérialisation des données du type d'appareil : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void UpdateTypeI_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = TypeInterventionDataGrid.SelectedItem as TypeIntervention;
            if (selectedStatus == null)
            {
                MessageBox.Show("Veuillez sélectionner un type Intervention à mettre à jour.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewTypeNameTextBox.Text))
            {
                MessageBox.Show("Veuillez entrer un nouveau nom d'intervention.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var updatedStatus = new TypeIntervention
            {
                NomType = NewTypeNameTextBox.Text
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token); // Ensure _token is set
                var json = JsonSerializer.Serialize(updatedStatus);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{_typeInterventionApiUrl}/{selectedStatus.TypeInterventionId}", content);
                response.EnsureSuccessStatusCode();

                MessageBox.Show("type d'intervention mis à jour avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadTypeInterventions(); // Reload statuses after update

                NewTypeNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour du type d'intervention : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de la sérialisation des données du type d'intervention : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void UpdateEmplacement_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = EmplacementDataGrid.SelectedItem as Emplacement;
            if (selectedStatus == null)
            {
                MessageBox.Show("Veuillez sélectionner un emplacement à mettre à jour.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewEmplacementNameTextBox.Text))
            {
                MessageBox.Show("Veuillez entrer un nouveau nom d'emplacement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var updatedStatus = new Emplacement
            {
                NomEmplacement = NewEmplacementNameTextBox.Text
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token); // Ensure _token is set
                var json = JsonSerializer.Serialize(updatedStatus);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{_statusApiUrl}/{selectedStatus.EmplacementId}", content);
                response.EnsureSuccessStatusCode();

                MessageBox.Show("Emplacement mis à jour avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadEmplacements(); // Reload statuses after update

                NewEmplacementNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour d'emplacement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de la sérialisation des données d'emplacement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }










        private async void AddEtage_Click(object sender, RoutedEventArgs e)
        {
            var name = NewEtageNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Veuillez entrer un nom pour le type d'intervention.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newEtage = new Etage { NomEtage = name };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var json = JsonSerializer.Serialize(newEtage);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_etageApiUrl, content);
                response.EnsureSuccessStatusCode();

                // Reload the interventions after adding
                LoadEtages();

                // Clear the TextBox after adding the new intervention
                NewEtageNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de l'étage : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteEtage_Click(object sender, RoutedEventArgs e)
        {
            var selectedEtage = EtageDataGrid.SelectedItem as Etage;
            if (selectedEtage == null)
            {
                MessageBox.Show("Veuillez sélectionner une intervention à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.DeleteAsync($"{_etageApiUrl}/{selectedEtage.EtageId}");
                response.EnsureSuccessStatusCode();

                // Reload the interventions after deletion
                LoadEtages();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du type d'intervention : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
                var outerObject = JsonSerializer.Deserialize<OuterObject<Emplacement>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var emplacements = outerObject?.Values ?? new List<Emplacement>();

                Emplacement.Clear();
                foreach (var emplacement in emplacements)
                {
                    Emplacement.Add(emplacement);
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

        private async void AddEmplacement_Click(object sender, RoutedEventArgs e)
        {
            var name = NewEmplacementNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Veuillez entrer un nom pour l'emplacement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newEmplacement = new Emplacement { NomEmplacement = name };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var json = JsonSerializer.Serialize(newEmplacement);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_emplacementApiUrl, content);
                response.EnsureSuccessStatusCode();

                // Reload the emplacements after adding
                LoadEmplacements();

                // Clear the TextBox after adding the new emplacement
                NewEmplacementNameTextBox.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de l'emplacement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteEmplacement_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmplacement = EmplacementDataGrid.SelectedItem as Emplacement;
            if (selectedEmplacement == null)
            {
                MessageBox.Show("Veuillez sélectionner un emplacement à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.DeleteAsync($"{_emplacementApiUrl}/{selectedEmplacement.EmplacementId}");
                response.EnsureSuccessStatusCode();

                // Reload the emplacements after deletion
                LoadEmplacements();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la suppression de l'emplacement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }







        private int SelectedTicketId = 0;


        private void TicketDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected ticket from the DataGrid
            var selectedTicket = TicketDataGrid.SelectedItem as Ticket;

            if (selectedTicket != null)
            {
                // Store the selected ticket's ID
                SelectedTicketId = selectedTicket.ticketId; // Assuming TicketId is a property in the Ticket model
            }
            else
            {
                // Handle the case when no ticket is selected
                SelectedTicketId = 0;
            }
        }



        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (TicketDataGrid.SelectedItem is Ticket selectedTicket)
            {
                // Pass the selected ticket and the Statuses list to the TicketDetailsWindow
                TicketDetailsWindow detailsWindow = new TicketDetailsWindow(selectedTicket);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un ticket pour voir les détails.", "Aucune sélection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private async void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the form data
            var description = NewTicketDescriptionTextBox.Text.Trim();
            var oralement = NewTicketOralementCheckBox.IsChecked ?? false; // Check if Oralement is checked
            var appareilNom = NewTicketAppareilNomTextBox.Text.Trim();
            var etage = NewTicketEtageComboBox.SelectedItem as Etage;
            var emplacement = NewTicketEmplacementComboBox.SelectedItem as Emplacement;
            var motif = NewTicketMotifTextBox.Text.Trim();
            var type = NewTicketTypeComboBox.SelectedItem as TypeIntervention; // Assuming you have TypeInterventions in the ViewModel
            var status = TicketStatusComboBox.SelectedItem as Status;
            var typeApp = NewTypeAppareilComboBox.SelectedItem as TypeAppareil; // Assuming you have TypeInterventions in the ViewModel

            // Get the selected date from the DatePicker
            var selectedDate = NewTicketDatePicker.SelectedDate ?? DateTime.Now;

            // Validate input
            if ( string.IsNullOrEmpty(motif) || type == null || etage == null || status == null || typeApp == null ||  emplacement == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new Ticket object to send to the API
            var newTicket = new TicketCreateDto
            {
                Description = description,
                Oralement = oralement,
                AppareilNom = appareilNom,
                EtageId = etage.EtageId,
                EmplacementId = emplacement.EmplacementId,
                TypeAppareilId = typeApp.TypeAppareilId,
                MotifDemande = motif,
                TypeInterventionId = type.TypeInterventionId,
                StatusId = status.StatusId,
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
                LoadTickets();

                // Clear the input fields
                NewTicketDescriptionTextBox.Clear();
                NewTicketOralementCheckBox.IsChecked = false;
                NewTicketAppareilNomTextBox.Clear();
                NewTicketEtageComboBox.SelectedIndex = -1;
                NewTicketEmplacementComboBox.SelectedIndex = -1;
                NewTicketMotifTextBox.Clear();
                NewTicketTypeComboBox.SelectedIndex = -1;
                NewTypeAppareilComboBox.SelectedIndex = -1;
                TicketStatusComboBox.SelectedIndex = -1;

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



        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            if (TicketDataGrid.ItemsSource == null)
            {
                MessageBox.Show("Aucune donnée à exporter.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var tickets = TicketDataGrid.ItemsSource.Cast<Ticket>().ToList(); // Replace TicketModel with your actual model

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Registre des interventions");
                    worksheet.Cell("B1").Value = "Registre des interventions informatiques";
                    worksheet.Cell("B5").Value = "Version : 01";
                    worksheet.Cell("B6").Value = "Date d'application : 07/07/2021";

                    // Headers
                    int headerRow = 8;
                    worksheet.Cell(headerRow, 1).Value = "Demande N°";

                    worksheet.Cell(headerRow, 1).Value = "Date et Heure de demande";
                    worksheet.Cell(headerRow, 2).Value = "Date et heure debut  d’intervention";
                    worksheet.Cell(headerRow, 3).Value = "Type matériel";
                    worksheet.Cell(headerRow, 4).Value = "Description de L’anomalie \r\n(+ Réf. machine ou appareil) ";
                    worksheet.Cell(headerRow, 5).Value = "Description de l’intervention";
                    worksheet.Cell(headerRow, 6).Value = "Date et heure fin intervention";
                    worksheet.Cell(headerRow, 7).Value = "Durée totale d’intervention";
                    worksheet.Cell(headerRow, 8).Value = "Nom Intervenant ";
                    worksheet.Cell(headerRow, 9).Value = "Visa du demandeur (validation de la réception)";



                    int row = headerRow + 1;


                    // Data
                    foreach (var ticket in tickets)
                    {
                        worksheet.Cell(row, 1).Value = ticket.DateCreation.ToString("yyyy/MM/dd HH:mm");
                        worksheet.Cell(row, 2).Value = ticket.DateCreation.ToString("yyyy/MM/dd HH:mm");
                        
                        worksheet.Cell(row, 3).Value = ticket.NomTypeAppareil;
                        worksheet.Cell(row, 4).Value = ticket.MotifDemande;
                        worksheet.Cell(row, 5).Value = ticket.Description;

                        worksheet.Cell(row, 6).Value = ticket.ValidationTime;
                        worksheet.Cell(row, 7).Value = ticket.Duration;

                        worksheet.Cell(row, 8).Value = ticket.Email;
                        
                        worksheet.Cell(row, 9).Value = ticket.Validation1 ? "Oui" : "Non";

                      

                        row++;
                    }

                    // Format the table
                    worksheet.Columns().AdjustToContents();

                    // Save File
                    var saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel Files (*.xlsx)|*.xlsx",
                        Title = "Enregistrer sous",
                        FileName = "Registre_Interventions.xlsx"
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Exportation réussie!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'exportation: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }














            private async void ValidateTicket_Click(object sender, RoutedEventArgs e)
        {
            var selectedTicket = TicketDataGrid.SelectedItem as Ticket;
            if (selectedTicket == null)
            {
                MessageBox.Show("Veuillez sélectionner un ticket à valider.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var validateTicketDto = new TicketUpdateDto
            {
                Validation1 = true
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var json = JsonSerializer.Serialize(validateTicketDto);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Validate the ticket via API
                var response = await client.PutAsync($"{_ticketApiUrl}/Validate/{selectedTicket.ticketId}", content);
                response.EnsureSuccessStatusCode();

                // Get the updated ticket details from the response
                var updatedTicketJson = await response.Content.ReadAsStringAsync();
                var updatedTicket = JsonSerializer.Deserialize<Ticket>(updatedTicketJson);

                // Update the selected ticket in the DataGrid
                if (updatedTicket != null)
                {
                    selectedTicket.Validation1 = updatedTicket.Validation1;
                    selectedTicket.ValidationTime = updatedTicket.ValidationTime;
                    selectedTicket.Duration = updatedTicket.Duration; // Update Duration property
                }

                MessageBox.Show("Ticket validé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                TicketDataGrid.Items.Refresh(); // Refresh DataGrid to display updated data
                LoadTickets();

            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la validation du ticket. {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de la sérialisation des données de validation. {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    // Ensure the Duration is correctly set (it should already be set by the API)
                    if (ticket.ValidationTime.HasValue)
                    {
                        ticket.Duration = ticket.ValidationTime.Value - ticket.DateCreation;
                    }
                    Tickets.Add(ticket);
                }

                TicketDataGrid.Items.Refresh(); // Refresh the DataGrid to reflect updated data
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la récupération des tickets. {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de l'analyse des données des tickets. {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private async void DeleteTicket_Click(object sender, RoutedEventArgs e)
        {
            var selectedTicket = TicketDataGrid.SelectedItem as Ticket;
            if (selectedTicket == null)
            {
                MessageBox.Show("Veuillez sélectionner un ticket à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.DeleteAsync($"{_ticketApiUrl}/{selectedTicket.ticketId}");
                response.EnsureSuccessStatusCode();

                // Reload the tickets after deletion
                LoadTickets();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du ticket. {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
                LoadUtilisateurs();
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



        private async void UpdateTicket_Click(object sender, RoutedEventArgs e)
        {
            var selectedTicket = TicketDataGrid.SelectedItem as Ticket;
            if (selectedTicket == null)
            {
                MessageBox.Show("Veuillez sélectionner un ticket à mettre à jour.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var status = TicketStatussComboBox.SelectedItem as Status;
            if (status == null)
            {
                MessageBox.Show("Veuillez sélectionner un statut valide pour le ticket.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var updatedTicket = new TicketUpdateDto
            {
                StatusId = status.StatusId,
            };

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var json = JsonSerializer.Serialize(updatedTicket);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{_ticketApiUrl}/{selectedTicket.ticketId}", content);
                response.EnsureSuccessStatusCode();
                MessageBox.Show("Ticket mis à jour avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadTickets();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour du ticket : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Erreur lors de la sérialisation des données du ticket : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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



        private void StatusDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NewTicketMotifTextBox_TextChanged(object sender, TextChangedEventArgs e)
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
    public class StatusUpdateDto
    {
        public int StatusId { get; set; }
        public string NomStatus { get; set; }
    }

    public class TicketUpdateDto
    {
        public int ticketId { get; set; }
        public int? StatusId { get; set; }
        public bool? Validation1 { get; set; }
       

       
        




   

    }


    // Ticket class
    public class Ticket
    {
        public int ticketId { get; set; }
        public string Email { get; set; }


        public string Description { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime? ValidationTime { get; set; }
        public bool Oralement { get; set; }
        public string AppareilNom { get; set; }
        public string? NomEtage { get; set; }
        public string NomEmplacement { get; set; }
        public bool Validation1 { get; set; }

        public int TypeInterventionId { get; set; }

        public string NomTypeAppareil { get; set; }
        public string MotifDemande { get; set; }
        public string NomType { get; set; }
        public string NomStatus { get; set; }
        public TimeSpan Duration { get; set; }

        public int StatusId { get; set; }
        // Nested objects
        public Status Status { get; set; }
        public TypeIntervention TypeIntervention { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public TypeAppareil TypeAppareil { get; set; }
        public string Efficacity
        {
            get
            {
                if (Duration == default) // Check if Duration is the default value
                    return "Pas Validée";

                double maxHours = NomStatus == "Urgent" ? 12 : 24;
                if (Duration.TotalHours <= maxHours)
                    return "Efficace";
                return "Inefficace";
            }
        }

        public Brush EfficacityColor
        {
            get
            {
                if (Duration == default) // Check if Duration is the default value
                    return Brushes.Gray; // Not Validated

                double maxHours = NomStatus == "Urgent" ? 12 : 24;
                if (Duration.TotalHours <= maxHours)
                    return Brushes.Green; // Effective
                return Brushes.DarkRed; // Ineffective
            }
        }


    }


    public class TicketCreateDto
    {
        public string Description { get; set; }
        public bool Oralement { get; set; }
        public string AppareilNom { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;

        public string MotifDemande { get; set; }
        public int TypeInterventionId { get; set; }
        public int StatusId { get; set; }
        public int? EtageId { get; set; }
        public int EmplacementId { get; set; }
        public int TypeAppareilId { get; set; }
        public bool Validation1 { get; set; }
        public bool Validation2 { get; set; }
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







    public class Emplacement
    {

        public int EmplacementId { get; set; }
        public string NomEmplacement { get; set; }

    }
    public class TypeAppareil
    {
        public int TypeAppareilId { get; set; }
        public string NomTypeAppareil { get; set; }
    }
    public class Etage
    {
        public int EtageId { get; set; }
        public string NomEtage { get; set; }
    }


}
