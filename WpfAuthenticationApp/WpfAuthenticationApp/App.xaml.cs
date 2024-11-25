using System.Net.Http;
using System.Windows;

namespace WpfAuthenticationApp
{
    public partial class App : Application
    {
        public static HttpClient HttpClient { get; private set; }

        public App()
        {
            // Initialisation de HttpClient
            HttpClient = new HttpClient();
        }
    }
}
