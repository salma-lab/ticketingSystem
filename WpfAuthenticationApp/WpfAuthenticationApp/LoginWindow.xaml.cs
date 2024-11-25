using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;


namespace WpfAuthenticationApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow() => InitializeComponent();

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text;
            var password = PasswordBox.Password;

            try
            {
                ErrorMessage.Visibility = Visibility.Collapsed; // Hide any previous errors

                // Authenticate the user
                var token = await AuthenticateUserAsync(email, password);

                // Extract the role from the token
                var role = GetUserRoleFromToken(token);

                // Open the appropriate dashboard based on the role
                if (role == "Admin")
                {
                    new AdminDashboard(token).Show();
                }
                else if (role == "User")
                {
                    new UserDashboard(token).Show();
                }
                else
                {
                    throw new Exception("Unrecognized role. Access denied.");
                }

                // Close the login window
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message; // Show error to the user
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Sends user credentials to the backend for authentication and retrieves a JWT token.
        /// </summary>
        private async Task<string> AuthenticateUserAsync(string email, string password)
        {
            using var client = new HttpClient();
            var url = "https://localhost:7046/api/Auth/login"; // Update with your backend's actual URL

            try
            {
                // Prepare the payload
                var payload = new { email, password };
                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
                    return tokenResponse.Token;
                }
                else
                {
                    // Log and throw an exception for HTTP errors
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Authentication failed. Server responded with: {response.StatusCode}. Details: {error}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to connect to the server. Ensure the API is running and accessible. Details: " + ex.Message);
            }
        }

        /// <summary>
        /// Extracts the role from a JWT token.
        /// </summary>
        private string GetUserRoleFromToken(string token)
        {
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value ?? "Unknown";
        }
    }

    /// <summary>
    /// Represents the response model for a token from the backend.
    /// </summary>
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
