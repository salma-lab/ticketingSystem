using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAuthenticationApp
{
    /// <summary>
    /// Logique d'interaction pour EditTicketWindow.xaml
    /// </summary>
    public partial class EditTicketWindow : Window
    {
        public Ticket Ticket { get; private set; }

        public EditTicketWindow(Ticket ticket)
        {
            InitializeComponent();
            Ticket = ticket;
            DataContext = Ticket; // Bind the Ticket object to the UI
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Close the window and indicate success
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Close the window without saving
            Close();
        }
    }

}
