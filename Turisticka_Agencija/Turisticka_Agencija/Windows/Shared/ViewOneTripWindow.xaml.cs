using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;

namespace Turisticka_Agencija.Windows.Shared
{
    /// <summary>
    /// Interaction logic for ViewOneTripWindow.xaml
    /// </summary>
    public partial class ViewOneTripWindow : Window
    {
        private Trip _trip;

        public ViewOneTripWindow()
        {
            InitializeComponent();
        }

        public ViewOneTripWindow(Trip trip)
        {
            InitializeComponent();

            _trip = trip;
            BuildMenuItems();
            FillChosenTripData();
            RefreshButtons();
        }

        private void BuildMenuItems()
        {
            Menu.Items.Clear();

            var helpMenuItem = new MenuItem
            {
                Header = "Pomoć",
                Name = "helpMenuItem",
            };
            helpMenuItem.Click += helpMenuItem_Click;
            Menu.Items.Add(helpMenuItem);
        }

        private void FillChosenTripData()
        {
            NameTextBox.Text = _trip.Name;
            FromDate.Text = _trip.Start.ToShortDateString();
            ToDate.Text = _trip.End.ToShortDateString();
            PriceTextBox.Text = _trip.Price + " RSD";
            DescriptionTextBox.Text = _trip.Description;

            AccommodationsTable.ItemsSource = _trip.Accommodations;
            RestaurantsTable.ItemsSource = _trip.Restaurants;
            PlacesTable.ItemsSource = _trip.Places;

            if (_trip.Accommodations != null)
            {
                foreach (Accommodation accommodation in _trip.Accommodations)
                {
                    var packIcon = new PackIcon();
                    packIcon.Kind = PackIconKind.Home;
                    AddPin(accommodation.Latitude, accommodation.Longitude, accommodation.Name, Brushes.IndianRed, packIcon);
                }
            }

            if (_trip.Restaurants != null)
            {
                foreach (Restaurant restaurant in _trip.Restaurants)
                {
                    var packIcon = new PackIcon();
                    packIcon.Kind = PackIconKind.Restaurant;
                    AddPin(restaurant.Latitude, restaurant.Longitude, restaurant.Name, Brushes.DarkCyan, packIcon);
                }
            }

            if (_trip.Places != null)
            {
                foreach (Place place in _trip.Places)
                {
                    var packIcon = new PackIcon();
                    packIcon.Kind = PackIconKind.Landscape;
                    AddPin(place.Latitude, place.Longitude, place.Name, Brushes.DarkOrange, packIcon);
                }
            }
        }

        private void AddPin(double latitude, double longitude, string heading, SolidColorBrush color, PackIcon icon)
        {
            var location = new Location(latitude, longitude);
            var pin = new Pushpin();
            pin.Location = location;
            pin.ToolTip = heading;
            pin.Content = icon;
            pin.Background = color;
            Map.Children.Add(pin);
            Map.Center = location;
        }

        private void helpMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDataGrid = (DataGrid)sender;

            // Check if a row is selected or deselected
            if (selectedDataGrid.SelectedItem != null)
            {
                // Deselect rows in other DataGrids
                if (selectedDataGrid == PlacesTable)
                {
                    RestaurantsTable.SelectedItem = null;
                    AccommodationsTable.SelectedItem = null;
                }
                else if (selectedDataGrid == RestaurantsTable)
                {
                    PlacesTable.SelectedItem = null;
                    AccommodationsTable.SelectedItem = null;
                }
                else if (selectedDataGrid == AccommodationsTable)
                {
                    PlacesTable.SelectedItem = null;
                    RestaurantsTable.SelectedItem = null;
                }

                ViewButton.IsEnabled = true;
            }
            else
            {
                ViewButton.IsEnabled = false;
            }
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!UserService.IsLoggedIn)
            {
                MessageBox.Show("Prvo se morate ulogovati ili registrovati (ako još nemate nalog).");
                return;
            }

            if (ReserveButtonText.Text.StartsWith("Otk"))
            {
                UserService.LoggedUser.CancelReservation(_trip);
                MessageBox.Show("Uspešno ste otkazali ovu rezervaciju.");
                RefreshButtons();
                return;
            }
            UserService.LoggedUser.ReserveTrip(_trip);
            MessageBox.Show("Uspešno ste rezervisali ovo putovanje!");
            RefreshButtons();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!UserService.IsLoggedIn)
            {
                MessageBox.Show("Prvo se morate ulogovati ili registrovati (ako još nemate nalog).");
                return;
            }

            var decision = MessageBox.Show("Da li ste sigurni da želite da kupite ovo putovanje?", 
                "Potvrda kupovine", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (decision == MessageBoxResult.No)
            {
                return;
            }
            UserService.LoggedUser.BuyTrip(_trip);
            MessageBox.Show("Uspešno ste kupili ovo putovanje!");
            RefreshButtons();
        }

        private void RefreshButtons()
        {
            if (!UserService.IsLoggedIn)
                return;

            ReserveButtonText.Text = "Rezerviši";

            if (UserService.LoggedUser.ReservedTrip(_trip))
            {
                ReserveButtonText.Text = "Otkaži rezervaciju";
            }

            if (UserService.LoggedUser.BoughtTrip(_trip))
            {
                ReserveButton.Visibility = Visibility.Collapsed;
                BuyButton.Visibility = Visibility.Collapsed;
                AlreadyBoughtTextBox.Visibility = Visibility.Visible;
            }
        }
    }
}
