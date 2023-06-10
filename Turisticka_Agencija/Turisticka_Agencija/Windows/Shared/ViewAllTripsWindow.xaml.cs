using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;

namespace Turisticka_Agencija.Windows.Shared
{
    /// <summary>
    /// Interaction logic for ViewAllTripsWindow.xaml
    /// </summary>
    public partial class ViewAllTripsWindow : Window
    {

        private readonly ObservableCollection<Trip> _trips = new();

        public ViewAllTripsWindow()
        {
            InitializeComponent();

            foreach (var trip in TripService.GetAll())
            {
                _trips.Add(trip);
            }

            TripsTable.ItemsSource = _trips;
            VirtualizingPanel.SetIsVirtualizing(TripsTable, true);
            VirtualizingPanel.SetVirtualizationMode(TripsTable, VirtualizationMode.Recycling);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new();
            window.Show();
            Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Registration window = new();
            window.Show();
            Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            UserService.LoggedUser = null;
            UserService.IsLoggedIn = false;

            ViewAllTripsWindow window = new();
            window.Show();
            Close();
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            ViewOneTripWindow window = new((Trip)TripsTable.SelectedItem);
            Hide();
            window.ShowDialog();
            Show();
        }

        private void TripsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTrip = (Trip) TripsTable.SelectedItem;
            if (selectedTrip == null)
            {
                ViewButton.IsEnabled = false;
                return;
            }

            ViewButton.IsEnabled = true;
        }

        private void TripsTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTrip = (Trip)TripsTable.SelectedItem;
            if (selectedTrip == null)
            {
                return;
            }

            ViewOneTripWindow window = new(selectedTrip);
            Hide();
            window.ShowDialog();
            Show();
        }
    }
}
