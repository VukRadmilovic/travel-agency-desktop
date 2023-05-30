using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using BingMapsRESTToolkit;
using Microsoft.Maps.MapControl.WPF;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Utils;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace Turisticka_Agencija
{
    /// <summary>
    /// Interaction logic for CRUDRestaurant.xaml
    /// </summary>
    public partial class CRUDRestaurantWindow : Window
    {
        private readonly ObservableCollection<Restaurant> _restaurants = new();
        public static RoutedCommand NavigateToCrudRestaurant = new();
        public static RoutedCommand NavigateToCrudAccommodation = new();
        public static RoutedCommand LogoutCommand = new();
        public static RoutedCommand ClearFieldsCommand = new();
        public static RoutedCommand SaveCommand = new();
        public static RoutedCommand ModifyCommand = new();
        public static RoutedCommand DeleteCommand = new();
        private Restaurant _selectedRestaurant;
        private Collection<Restaurant> _searchedRestaurants = new();
        private CustomDataContext dataContext = new();
        public CRUDRestaurantWindow()
        {

            InitializeComponent();
            foreach(var restaurant in RestaurantService.GetAll()) _restaurants.Add(restaurant);
            RestaurantsTable.ItemsSource = _restaurants;
            VirtualizingStackPanel.SetIsVirtualizing(RestaurantsTable, true);
            VirtualizingStackPanel.SetVirtualizationMode(RestaurantsTable, VirtualizationMode.Recycling);
            NavigateToCrudRestaurant.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Alt));
            NavigateToCrudAccommodation.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
            ClearFieldsCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            ModifyCommand.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            DeleteCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            LogoutCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Alt));
            DataContext = dataContext;
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            Close();
        }

        private void RestaurantCRUD_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void AccommodationCRUD_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Accommodation");
        }

        private void ClearFieldsButton_OnClick(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void Search()
        {
            _searchedRestaurants.Clear();
            bool? isVeganFriendlyChecked = IsVeganFriendlyFilterCb.IsChecked;
            bool? isGlutenFreeFriendlyChecked = IsGlutenFreeFriendlyFilterCb.IsChecked;
            string nameField = NameFilterField.Text;

            if (!nameField.Equals(""))
            {
                if (isVeganFriendlyChecked != null && isGlutenFreeFriendlyChecked != null)
                {
                    foreach (var restaurant in _restaurants)
                    {
                        if (restaurant.Name.IndexOf(NameFilterField.Text, StringComparison.OrdinalIgnoreCase) >= 0 &&
                            restaurant.IsGlutenFreeFriendly == isGlutenFreeFriendlyChecked &&
                            restaurant.IsVeganFriendly == isVeganFriendlyChecked)
                        {
                            _searchedRestaurants.Add(restaurant);
                        }
                    }
                }

                else if (isVeganFriendlyChecked != null && isGlutenFreeFriendlyChecked == null)
                {
                    foreach (var restaurant in _restaurants)
                    {
                        if (restaurant.Name.IndexOf(NameFilterField.Text, StringComparison.OrdinalIgnoreCase) >= 0 &&
                            restaurant.IsVeganFriendly == isVeganFriendlyChecked)
                        {
                            _searchedRestaurants.Add(restaurant);
                        }
                    }
                }

                else if (isVeganFriendlyChecked == null && isGlutenFreeFriendlyChecked != null)
                {
                    foreach (var restaurant in _restaurants)
                    {
                        if (restaurant.Name.IndexOf(NameFilterField.Text, StringComparison.OrdinalIgnoreCase) >= 0 &&
                            restaurant.IsGlutenFreeFriendly == isGlutenFreeFriendlyChecked)
                        {
                            _searchedRestaurants.Add(restaurant);
                        }
                    }
                }
                else
                {
                    foreach (var restaurant in _restaurants)
                    {
                        if (restaurant.Name.IndexOf(NameFilterField.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            _searchedRestaurants.Add(restaurant);
                        }
                    }
                }
            }
            else
            {
                if (isVeganFriendlyChecked != null && isGlutenFreeFriendlyChecked != null)
                {
                    foreach (var restaurant in _restaurants)
                    {
                        if (restaurant.IsGlutenFreeFriendly == isGlutenFreeFriendlyChecked &&
                            restaurant.IsVeganFriendly == isVeganFriendlyChecked)
                        {
                            _searchedRestaurants.Add(restaurant);
                        }
                    }
                }
                else if (isVeganFriendlyChecked != null && isGlutenFreeFriendlyChecked == null)
                {
                    foreach (var restaurant in _restaurants)
                    {
                        if (restaurant.IsVeganFriendly == isVeganFriendlyChecked)
                        {
                            _searchedRestaurants.Add(restaurant);
                        }
                    }
                }

                else if (isVeganFriendlyChecked == null && isGlutenFreeFriendlyChecked != null)
                {
                    foreach (var restaurant in _restaurants)
                    {
                        if (restaurant.IsGlutenFreeFriendly == isGlutenFreeFriendlyChecked)
                        {
                            _searchedRestaurants.Add(restaurant);
                        }
                    }
                }
                else _searchedRestaurants = new ObservableCollection<Restaurant>(_restaurants);
            }

            //MessageBox.Show(_restaurants.Count.ToString());
            RestaurantsTable.ItemsSource = _searchedRestaurants;
        }

        private void ClearFields()
        {
            var dataContext = DataContext as CustomDataContext;
            dataContext.IsModifyMode = false;
            dataContext.CurrentRestaurant = new Restaurant();
            dataContext.SelectedAddress = new AutocompleteProvider.Address();
            RestaurantService.Address = dataContext.SelectedAddress;
            DataContext = dataContext;
            _selectedRestaurant = null;
            RestaurantsTable.UnselectAllCells();
            NameField.Text = "";
            LocationField.Text = "";
            InfoLinkField.Text = "";
            DescriptionField.Text = "";
            IsGlutenFreeFriendlyCb.IsChecked = false;
            IsVeganFriendlyCb.IsChecked = false;

            Map.Children.Clear();
            DeleteButton.IsEnabled = false;
        }


        private void FillFields(Restaurant selected)
        {
            var dataContext = DataContext as CustomDataContext;
            dataContext.CurrentRestaurant = selected;
            AutocompleteProvider.Address copyAddress = new AutocompleteProvider.Address();
            copyAddress.FormattedAddress = selected.Address;
            dataContext.SelectedAddress = copyAddress;
            dataContext.IsModifyMode = true;
            DataContext = dataContext;

            NameField.Text = selected.Name;
            LocationField.Text = selected.Address;
            InfoLinkField.Text = selected.InfoLink;
            DescriptionField.Text = selected.Description;
            IsGlutenFreeFriendlyCb.IsChecked = selected.IsGlutenFreeFriendly;
            IsVeganFriendlyCb.IsChecked = selected.IsVeganFriendly;


            MakeNewPin(new List<double> {selected.Latitude,selected.Longitude});
            RestaurantService.Address.FormattedAddress = selected.Address;
            _selectedRestaurant = selected;
        }

        private void MakeNewPin(List<double> coordinates)
        {
            Map.Children.Clear();
            Location newLocation = new Location(coordinates[0], coordinates[1]);
            Pushpin location = new Pushpin();
            location.Location = newLocation;
            Map.Children.Add(location);
            Map.Center = newLocation;
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!CreateButton.IsEnabled) return;
            if (dataContext.SelectedAddress.FormattedAddress == "" || dataContext.SelectedAddress == null)
            {
                MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List<double> coordinates = RestaurantService.GeocodeAddress();
            if (coordinates == null)
            {
                MessageBox.Show("Nepostojeća adresa.","Greška",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataContext.CurrentRestaurant.Latitude = coordinates[0];
            dataContext.CurrentRestaurant.Longitude = coordinates[1];
            dataContext.CurrentRestaurant.IsGlutenFreeFriendly = IsGlutenFreeFriendlyCb.IsChecked == true;
            dataContext.CurrentRestaurant.IsVeganFriendly = IsVeganFriendlyCb.IsChecked == true;
            dataContext.CurrentRestaurant.Address = dataContext.SelectedAddress.FormattedAddress;
            RestaurantService.Save(dataContext.CurrentRestaurant);
            _restaurants.Insert(0,dataContext.CurrentRestaurant);
            ClearFields();
            NameField.Focus();
            if (SuccessSnackbar.MessageQueue is { } messageQueue)
            {
                Task.Factory.StartNew(() => messageQueue.Enqueue("Restoran uspešno dodat."));
            }
        }

        private async void LocationField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!dataContext.IsModifyMode && (dataContext.SelectedAddress == null ||
                                              dataContext.SelectedAddress.FormattedAddress == ""))
            {
                MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dataContext.IsModifyMode && (dataContext.SelectedAddress == null ||
                                             RestaurantService.Address.FormattedAddress == dataContext.SelectedAddress.FormattedAddress ))
            {
                MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RestaurantService.Address = dataContext.SelectedAddress;
            Map.Children.Clear();
            List<double> coordinates;
            coordinates = await Task.Run(() => RestaurantService.GeocodeAddress());
            if (coordinates != null)
            {
                MakeNewPin(coordinates);
                return;
            }
            MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void RestaurantsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant selectedRestaurant = (Restaurant)RestaurantsTable.SelectedItem;
            if (selectedRestaurant == null) return;
            FillFields(selectedRestaurant);
            DeleteButton.IsEnabled = true;
            NameField.Focus();
            NameField.CaretIndex = selectedRestaurant.Name.Length;
        }

        private class CustomDataContext : INotifyPropertyChanged
        {
            private AutocompleteProvider.Address _selectedAddress;
            private Restaurant _currentRestaurant;
            private bool _isModifyMode;

            public bool IsModifyMode
            {
                get { return _isModifyMode; }
                set
                {
                    _isModifyMode = value;
                    OnPropertyChanged("IsModifyMode");
                }
            }

            public Restaurant CurrentRestaurant
            {
                get { return _currentRestaurant; }
                set
                {
                    _currentRestaurant = value;
                    OnPropertyChanged("CurrentRestaurant");
                }
            }

            public AutocompleteProvider.Address SelectedAddress
            {
                get { return _selectedAddress; }
                set
                {
                    _selectedAddress = value;
                    OnPropertyChanged("SelectedAddress");
                }
            }

            private void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            public CustomDataContext()
            {
                CurrentRestaurant = new Restaurant();
                SelectedAddress = new AutocompleteProvider.Address();
                IsModifyMode = false;
            }

            public CustomDataContext(CustomDataContext copy)
            {
                CurrentRestaurant = copy.CurrentRestaurant;
                SelectedAddress = copy.SelectedAddress;
                IsModifyMode = copy.IsModifyMode;
            }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DeleteButton.IsEnabled) return;
            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da obrišete označeni restoran?" +
                                                      " Nakon brisanja, podaci o restoranu će trajno biti obrisani.", "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Question);
            MessageBoxManager.Yes = "Da";
            MessageBoxManager.No = "Ne";
            if (result == MessageBoxResult.Yes)
            {
                RestaurantService.Delete(_selectedRestaurant);
                if (SuccessSnackbar.MessageQueue is { } messageQueue)
                {
                    Task.Factory.StartNew(() => messageQueue.Enqueue("Restoran uspešno obrisan."));
                }
                _restaurants.Remove(_selectedRestaurant);
                ClearFields();
            }
        }

        private void LocationField_TextInput(object sender, TextCompositionEventArgs e)
        {
        }

        private void LocationField_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
        }

        private void NameField_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show(dataContext.SelectedAddress.FormattedAddress);
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ModifyButton.IsEnabled) return;
            dataContext.CurrentRestaurant = _selectedRestaurant;
            if (dataContext.SelectedAddress == null || dataContext.SelectedAddress.FormattedAddress == "")
            {
                MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (RestaurantService.Address.FormattedAddress != dataContext.CurrentRestaurant.Address)
            {
                List<double> coordinates = RestaurantService.GeocodeAddress();
                if (coordinates == null)
                {
                    MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataContext.CurrentRestaurant.Latitude = coordinates[0];
                dataContext.CurrentRestaurant.Longitude = coordinates[1];
            }

            dataContext.CurrentRestaurant.IsGlutenFreeFriendly = IsGlutenFreeFriendlyCb.IsChecked == true;
            dataContext.CurrentRestaurant.IsVeganFriendly = IsVeganFriendlyCb.IsChecked == true;
            dataContext.CurrentRestaurant.Address = LocationField.Text;
            RestaurantsTable.Items.Refresh();
            RestaurantService.Update(dataContext.CurrentRestaurant);
            ClearFields();
            NameField.Focus();
            if (SuccessSnackbar.MessageQueue is { } messageQueue)
            {
                Task.Factory.StartNew(() => messageQueue.Enqueue("Restoran uspešno izmenjen."));
            }
        }

        private void NameFilterField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void IsVeganFriendlyFilterCb_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void IsGlutenFreeFriendlyFilterCb_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
    }
}
