using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Windows.Shared;

namespace Turisticka_Agencija.Windows.Admin
{
    /// <summary>
    /// Interaction logic for CRUDTripWindow.xaml
    /// </summary>
    public partial class CRUDTripWindow : Window
    {
        public static RoutedCommand NavigateToCrudRestaurant { get; } = new();
        public static RoutedCommand NavigateToCrudAccommodation { get; } = new();
        public static RoutedCommand NavigateToCrudPlace { get; } = new();
        public static RoutedCommand LogoutCommand { get; } = new();
        public static RoutedCommand ClearFieldsCommand { get; } = new();
        public static RoutedCommand SaveCommand { get; } = new();
        public static RoutedCommand ModifyCommand { get; } = new();
        public static RoutedCommand DeleteCommand { get; } = new();

        private ObservableCollection<Place> _currentPlaces = new();
        private ObservableCollection<Restaurant> _currentRestaurants = new();
        private ObservableCollection<Accommodation> _currentAccommodations = new();

        public CRUDTripWindow()
        {
            InitializeComponent();

            NavigateToCrudRestaurant.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Alt));
            NavigateToCrudAccommodation.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            NavigateToCrudPlace.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
            LogoutCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Alt));
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            ModifyCommand.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            DeleteCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            ClearFieldsCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));

            Loaded += (sender, e) => FocusManager.SetFocusedElement(this, TripsTable);

            TripsTable.ItemsSource = TripService.GetAll();
            AvailablePlacesTable.ItemsSource = PlaceService.GetAll();
            AvailableRestaurantsTable.ItemsSource = RestaurantService.GetAll();
            AvailableAccommodationsTable.ItemsSource = AccommodationService.GetAll();
        }

        private void TripsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTrip = (Trip)TripsTable.SelectedItem;

            if (selectedTrip == null)
            {
                DeleteButton.IsEnabled = false;
                ModifyButton.IsEnabled = false;
                return;
            }

            DeleteButton.IsEnabled = true;
            ModifyButton.IsEnabled = true;

            NameTextBox.Text = selectedTrip.Name;
            PriceTextBox.Text = selectedTrip.Price.ToString();
            FromDate.Text = selectedTrip.Start.ToShortDateString();
            ToDate.Text = selectedTrip.End.ToShortDateString();
            SoldTextBox.Text = TripBoughtOrReservedByUserService.NumberOfSold(selectedTrip).ToString();
            DescriptionTextBox.Text = selectedTrip.Description;

            _currentPlaces = new ObservableCollection<Place>(selectedTrip.Places);
            _currentRestaurants = new ObservableCollection<Restaurant>(selectedTrip.Restaurants);
            _currentAccommodations = new ObservableCollection<Accommodation>(selectedTrip.Accommodations);
            
            ThisTripPlacesTable.ItemsSource = _currentPlaces;
            ThisTripRestaurantsTable.ItemsSource = _currentRestaurants;
            ThisTripAccommodationsTable.ItemsSource = _currentAccommodations;

            Map.Children.Clear();

            if (selectedTrip.Accommodations != null)
            {
                foreach (Accommodation accommodation in selectedTrip.Accommodations)
                {
                    var packIcon = new PackIcon();
                    packIcon.Kind = PackIconKind.Home;
                    AddPin(accommodation.Latitude, accommodation.Longitude, accommodation.Name, Brushes.IndianRed, packIcon);
                }
            }

            if (selectedTrip.Restaurants != null)
            {
                foreach (Restaurant restaurant in selectedTrip.Restaurants)
                {
                    var packIcon = new PackIcon();
                    packIcon.Kind = PackIconKind.Restaurant;
                    AddPin(restaurant.Latitude, restaurant.Longitude, restaurant.Name, Brushes.DarkCyan, packIcon);
                }
            }

            if (selectedTrip.Places != null)
            {
                foreach (Place place in selectedTrip.Places)
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

        private void RestaurantCRUD_OnClick(object sender, RoutedEventArgs e)
        {
            var restaurantWindow = new CRUDRestaurantWindow();
            restaurantWindow.Show();
            Close();
        }

        private void AccommodationCRUD_OnClick(object sender, RoutedEventArgs e)
        {
            var accommodationWindow = new CRUDAccommodationWindow();
            accommodationWindow.Show();
            Close();
        }

        private void PlaceCRUD_Click(object sender, RoutedEventArgs e)
        {
            var placeWindow = new CRUDPlaceWindow();
            placeWindow.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl)) return;
            if (e.Key == Key.Left)
            {
                // Navigate to the previous tab
                int selectedIndex = TabControl.SelectedIndex;
                if (selectedIndex > 0)
                    TabControl.SelectedIndex = selectedIndex - 1;
            }
            else if (e.Key == Key.Right)
            {
                // Navigate to the next tab
                int selectedIndex = TabControl.SelectedIndex;
                if (selectedIndex < TabControl.Items.Count - 1)
                    TabControl.SelectedIndex = selectedIndex + 1;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ClearButton.IsEnabled)
            {
                return;
            }
            ClearFields();
        }

        private void ClearFields()
        {
            NameTextBox.Clear();
            PriceTextBox.Clear();
            FromDate.Clear();
            ToDate.Clear();
            SoldTextBox.Clear();
            DescriptionTextBox.Clear();

            _currentPlaces.Clear();
            _currentRestaurants.Clear();
            _currentAccommodations.Clear();

            Map.Children.Clear();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SaveButton.IsEnabled)
            {
                return;
            }

            if (NameTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Morate uneti naziv putovanja.");
                return;
            }

            DateTime from;
            DateTime to;
            double price;

            string fromDate = FromDate.Text;
            string toDate = ToDate.Text;

            if (fromDate.EndsWith("."))
            {
                fromDate = fromDate.Substring(0, fromDate.Length - 1);
            }

            if (toDate.EndsWith("."))
            {
                toDate = toDate.Substring(0, toDate.Length - 1);
            }

            bool fromParsed = DateTime.TryParseExact(fromDate, "d.M.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out from);
            bool toParsed = DateTime.TryParseExact(toDate, "d.M.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out to);

            if (!fromParsed || !toParsed)
            {
                MessageBox.Show("Morate uneti datume u formatu dan.mesec.godina (npr. 23.5.2022)");
                return;
            }

            if (from > to)
            {
                MessageBox.Show("Datum polaska je posle datuma dolaska. Molim Vas proverite datume ponovo.");
                return;
            }

            bool priceParsed = double.TryParse(PriceTextBox.Text, out price);
            if (!priceParsed)
            {
                MessageBox.Show("Morate uneti cenu kao decimalan broj.");
                return;
            }

            Trip trip = new Trip(0, NameTextBox.Text, from, to, price, DescriptionTextBox.Text,
                _currentPlaces, _currentAccommodations, _currentRestaurants);
            bool success = TripService.Save(trip);
            if (!success)
            {
                MessageBox.Show("Došlo je do greške prilikom upisa podataka. Molim Vas pokušajte ponovo uskoro.");
                return;
            }

            ClearFields();
            NameTextBox.Focus();
            TripsTable.ItemsSource = TripService.GetAll();
            if (SuccessSnackbar.MessageQueue is { } messageQueue)
                Task.Factory.StartNew(() => messageQueue.Enqueue("Putovanje uspešno dodato."));
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ModifyButton.IsEnabled)
            {
                return;
            }

            var selectedTrip = (Trip)TripsTable.SelectedItem;

            if (selectedTrip == null)
            {
                MessageBox.Show("Morate odabrati koje putovanje želite da izmenite iz tabele.");
                return;
            }

            if (NameTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Morate uneti naziv putovanja.");
                return;
            }

            DateTime from;
            DateTime to;
            double price;

            string fromDate = FromDate.Text;
            string toDate = ToDate.Text;

            if (fromDate.EndsWith("."))
            {
                fromDate = fromDate.Substring(0, fromDate.Length - 1);
            }

            if (toDate.EndsWith("."))
            {
                toDate = toDate.Substring(0, toDate.Length - 1);
            }

            bool fromParsed = DateTime.TryParseExact(fromDate, "d.M.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out from);
            bool toParsed = DateTime.TryParseExact(toDate, "d.M.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out to);

            if (!fromParsed || !toParsed)
            {
                MessageBox.Show("Morate uneti datume u formatu dan.mesec.godina (npr. 23.5.2022)");
                return;
            }

            if (from > to)
            {
                MessageBox.Show("Datum polaska je posle datuma dolaska. Molim Vas proverite datume ponovo.");
                return;
            }

            bool priceParsed = double.TryParse(PriceTextBox.Text, out price);
            if (!priceParsed)
            {
                MessageBox.Show("Morate uneti cenu kao decimalan broj.");
                return;
            }

            selectedTrip.Name = NameTextBox.Text;
            selectedTrip.Start = from;
            selectedTrip.End = to;
            selectedTrip.Price = price;
            selectedTrip.Description = DescriptionTextBox.Text;
            selectedTrip.Places = _currentPlaces;
            selectedTrip.Accommodations = _currentAccommodations;
            selectedTrip.Restaurants = _currentRestaurants;

            bool success = TripService.Update(selectedTrip);
            if (!success)
            {
                MessageBox.Show("Došlo je do greške prilikom izmene podataka. Molim Vas pokušajte ponovo uskoro.");
                return;
            }

            ClearFields();
            TripsTable.ItemsSource = TripService.GetAll();
            if (SuccessSnackbar.MessageQueue is { } messageQueue)
                Task.Factory.StartNew(() => messageQueue.Enqueue("Putovanje uspešno izmenjeno."));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DeleteButton.IsEnabled)
            {
                return;
            }

            var selectedTrip = (Trip)TripsTable.SelectedItem;

            if (selectedTrip == null)
            {
                MessageBox.Show("Morate odabrati koje putovanje želite da obrišete iz tabele.");
                return;
            }

            var decision = MessageBox.Show("Da li ste sigurni da želite da trajno obrišete ovo putovanje?",
                "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (decision == MessageBoxResult.No)
            {
                return;
            }

            bool success = TripService.Delete(selectedTrip);
            if (!success)
            {
                MessageBox.Show("Došlo je do greške prilikom brisanja podataka. Molim Vas pokušajte ponovo uskoro.");
                return;
            }

            ClearFields();
            TripsTable.ItemsSource = TripService.GetAll();
            if (SuccessSnackbar.MessageQueue is { } messageQueue)
                Task.Factory.StartNew(() => messageQueue.Enqueue("Putovanje uspešno obrisano."));
        }
    }
}
