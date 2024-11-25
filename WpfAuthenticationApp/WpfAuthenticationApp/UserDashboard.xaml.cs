using System.Windows;

namespace WpfAuthenticationApp
{
    public partial class UserDashboard : Window
    {
        public UserDashboard(string token)
        {
            InitializeComponent();
            // Token can be used for authenticated actions in the dashboard
        }
    }
}
