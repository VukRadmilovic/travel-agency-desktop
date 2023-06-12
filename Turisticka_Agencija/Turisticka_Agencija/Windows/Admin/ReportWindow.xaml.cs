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
using Turisticka_Agencija.Windows.Shared;

namespace Turisticka_Agencija.Windows.Admin
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private readonly ObservableCollection<Trip> _trips = new();
        public ReportWindow()
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

        private void TableFilterField_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*var selectedAccommodation = (Accommodation)AccommodationsTable.SelectedItem;
            if (selectedAccommodation == null) return;
            FillFields(selectedAccommodation);
            DeleteButton.IsEnabled = true;
            NameField.Focus();
            NameField.CaretIndex = selectedAccommodation.Name.Length;*/
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void PlaceCRUD_OnClick(object sender, RoutedEventArgs e)
        {
            var placeWindow = new CRUDPlaceWindow();
            placeWindow.Show();
            Close();
        }
        private void RestaurantCRUD_OnClick(object sender, RoutedEventArgs e)
        {
            var restaurantsWindow = new CRUDRestaurantWindow();
            restaurantsWindow.Show();
            Close();
        }
        private void TripCRUD_Click(object sender, RoutedEventArgs e)
        {
            var window = new CRUDTripWindow();
            window.Show();
            Close();
        }

        private void Report(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow();
            reportWindow.Show();
            Close();
        }
    }
}
