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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
                    AddPin(accommodation.Latitude, accommodation.Longitude, accommodation.Name, Brushes.Red);
                }
            }

            if (_trip.Restaurants != null)
            {
                foreach (Restaurant restaurant in _trip.Restaurants)
                {
                    AddPin(restaurant.Latitude, restaurant.Longitude, restaurant.Name, Brushes.Blue);
                }
            }

            if (_trip.Places != null)
            {
                foreach (Place place in _trip.Places)
                {
                    AddPin(place.Latitude, place.Longitude, place.Name, Brushes.Green);
                }
            }
        }

        private void AddPin(double latitude, double longitude, string heading, SolidColorBrush color)
        {
            var location = new Location(latitude, longitude);
            var pin = new Pushpin();
            pin.Location = location;
            pin.ToolTip = heading;
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

    }
}
