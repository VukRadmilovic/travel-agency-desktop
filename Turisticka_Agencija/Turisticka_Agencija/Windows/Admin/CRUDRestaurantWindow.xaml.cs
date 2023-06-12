using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using BingMapsRESTToolkit;
using Microsoft.Maps.MapControl.WPF;
using Turisticka_Agencija.Help;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Windows.Admin;
using Turisticka_Agencija.Windows.Shared;
using Address = Turisticka_Agencija.Services.Address;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace Turisticka_Agencija;

/// <summary>
///     Interaction logic for CRUDRestaurant.xaml
/// </summary>
public partial class CRUDRestaurantWindow : Window
{
    private void HelpClick(object sender, ExecutedRoutedEventArgs e)
    {
        HelpProvider.ShowHelp("HelpCRUDR");
    }
    private readonly ObservableCollection<Restaurant> _restaurants = new();
    private ObservableCollection<Restaurant> _searchedRestaurants = new();
    private Restaurant _selectedRestaurant;
    private readonly CustomDataContext _dataContext = new();
    public static RoutedCommand NavigateToCrudRestaurant { get; } = new();
    public static RoutedCommand NavigateToCrudTrip { get; } = new();
    public static RoutedCommand NavigateToCrudAccommodation { get; } = new();
    public static RoutedCommand NavigateToCrudPlace { get; } = new();
    public static RoutedCommand LogoutCommand { get; } = new();
    public static RoutedCommand ClearFieldsCommand { get; } = new();
    public static RoutedCommand SaveCommand { get; } = new();
    public static RoutedCommand ModifyCommand { get; } = new();
    public static RoutedCommand DeleteCommand { get; } = new();
    public static RoutedCommand SearchCommand { get; } = new();

    public CRUDRestaurantWindow()
    {
        InitializeComponent();
        foreach (var restaurant in RestaurantService.GetAll()) _restaurants.Add(restaurant);
        RestaurantsTable.ItemsSource = _restaurants;
        VirtualizingPanel.SetIsVirtualizing(RestaurantsTable, true);
        VirtualizingPanel.SetVirtualizationMode(RestaurantsTable, VirtualizationMode.Recycling);
        DataContext = _dataContext;

        NavigateToCrudTrip.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
        NavigateToCrudAccommodation.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
        NavigateToCrudPlace.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
        ClearFieldsCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
        SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
        SearchCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
        ModifyCommand.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
        DeleteCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
        LogoutCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Alt));
    }

    private void Logout(object sender, RoutedEventArgs e)
    {
        var loginWindow = new LoginWindow();
        loginWindow.Show();
        Close();
    }

    private void SearchShortcut(object sender, RoutedEventArgs e)
    {
        NameFilterField.Clear();
        NameFilterField.Focus();
    }

    private void AccommodationCRUD_OnClick(object sender, RoutedEventArgs e)
    {
        var accommodationWindow = new CRUDAccommodationWindow();
        accommodationWindow.Show();
        Close();
    }

    private void ClearFieldsButton_OnClick(object sender, RoutedEventArgs e)
    {
        ClearFields();
    }

    private void Search()
    {
        _searchedRestaurants.Clear();
        var isVeganFriendlyChecked = IsVeganFriendlyFilterCb.IsChecked;
        var isGlutenFreeFriendlyChecked = IsGlutenFreeFriendlyFilterCb.IsChecked;
        var nameField = NameFilterField.Text;
        var addressField = AddressFilterField.Text;

        if (nameField != "")
        {
            if (isVeganFriendlyChecked != null && isGlutenFreeFriendlyChecked != null)
            {
                foreach (var restaurant in _restaurants)
                    if (restaurant.Name.IndexOf(nameField, StringComparison.OrdinalIgnoreCase) >= 0 &&
                        restaurant.IsGlutenFreeFriendly == isGlutenFreeFriendlyChecked &&
                        restaurant.IsVeganFriendly == isVeganFriendlyChecked)
                        _searchedRestaurants.Add(restaurant);
            }

            else if (isVeganFriendlyChecked != null && isGlutenFreeFriendlyChecked == null)
            {
                foreach (var restaurant in _restaurants)
                    if (restaurant.Name.IndexOf(nameField, StringComparison.OrdinalIgnoreCase) >= 0 &&
                        restaurant.IsVeganFriendly == isVeganFriendlyChecked)
                        _searchedRestaurants.Add(restaurant);
            }

            else if (isVeganFriendlyChecked == null && isGlutenFreeFriendlyChecked != null)
            {
                foreach (var restaurant in _restaurants)
                    if (restaurant.Name.IndexOf(nameField, StringComparison.OrdinalIgnoreCase) >= 0 &&
                        restaurant.IsGlutenFreeFriendly == isGlutenFreeFriendlyChecked)
                        _searchedRestaurants.Add(restaurant);
            }
            else
            {
                foreach (var restaurant in _restaurants)
                    if (restaurant.Name.IndexOf(nameField, StringComparison.OrdinalIgnoreCase) >= 0)
                        _searchedRestaurants.Add(restaurant);
            }
        }
        else
        {
            if (isVeganFriendlyChecked != null && isGlutenFreeFriendlyChecked != null)
            {
                foreach (var restaurant in _restaurants)
                    if (restaurant.IsGlutenFreeFriendly == isGlutenFreeFriendlyChecked &&
                        restaurant.IsVeganFriendly == isVeganFriendlyChecked)
                        _searchedRestaurants.Add(restaurant);
            }
            else if (isVeganFriendlyChecked != null && isGlutenFreeFriendlyChecked == null)
            {
                foreach (var restaurant in _restaurants)
                    if (restaurant.IsVeganFriendly == isVeganFriendlyChecked)
                        _searchedRestaurants.Add(restaurant);
            }

            else if (isVeganFriendlyChecked == null && isGlutenFreeFriendlyChecked != null)
            {
                foreach (var restaurant in _restaurants)
                    if (restaurant.IsGlutenFreeFriendly == isGlutenFreeFriendlyChecked)
                        _searchedRestaurants.Add(restaurant);
            }
            else
            {
                _searchedRestaurants = new ObservableCollection<Restaurant>(_restaurants);
            }
        }

        ObservableCollection<Restaurant> finalFilter = new();
        if (addressField != "")
        {
            foreach (var restaurant in _searchedRestaurants)
            {
                if (restaurant.Address.IndexOf(addressField, StringComparison.OrdinalIgnoreCase) >= 0)
                    finalFilter.Add(restaurant);
            }
        }
        else finalFilter = _searchedRestaurants;

        RestaurantsTable.ItemsSource = null;
        RestaurantsTable.ItemsSource = finalFilter;
    }

    private void ClearFields()
    {
        var dataContext = DataContext as CustomDataContext;
        dataContext.IsModifyMode = false;
        dataContext.CurrentRestaurant = new Restaurant();
        dataContext.SelectedAddress = new Address();
        MapService.Address = dataContext.SelectedAddress;
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
        NameField.Focus();
    }


    private void FillFields(Restaurant selected)
    {
        var dataContext = DataContext as CustomDataContext;
        dataContext.CurrentRestaurant = selected;
        Address copyAddress = new Address();
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


        MakeNewPin(new List<double> { selected.Latitude, selected.Longitude });
        MapService.Address.FormattedAddress = selected.Address;
        _selectedRestaurant = selected;
    }

    private void MakeNewPin(List<double> coordinates)
    {
        Map.Children.Clear();
        var newLocation = new Location(coordinates[0], coordinates[1]);
        var location = new Pushpin();
        location.Location = newLocation;
        Map.Children.Add(location);
        Map.Center = newLocation;
    }

    private void CreateButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!CreateButton.IsEnabled) return;
        if (_dataContext.SelectedAddress.FormattedAddress == "" || _dataContext.SelectedAddress == null)
        {
            MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        List<double> coordinates = MapService.GeocodeAddress();
        if (coordinates == null)
        {
            MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        _dataContext.CurrentRestaurant.Latitude = coordinates[0];
        _dataContext.CurrentRestaurant.Longitude = coordinates[1];
        _dataContext.CurrentRestaurant.IsGlutenFreeFriendly = IsGlutenFreeFriendlyCb.IsChecked == true;
        _dataContext.CurrentRestaurant.IsVeganFriendly = IsVeganFriendlyCb.IsChecked == true;
        _dataContext.CurrentRestaurant.Address = _dataContext.SelectedAddress.FormattedAddress;
        RestaurantService.Save(_dataContext.CurrentRestaurant);
        _restaurants.Insert(0, _dataContext.CurrentRestaurant);
        ClearFields();
        NameField.Focus();
        if (SuccessSnackbar.MessageQueue is { } messageQueue)
            Task.Factory.StartNew(() => messageQueue.Enqueue("Restoran uspešno dodat."));
    }

    private async void LocationField_LostFocus(object sender, RoutedEventArgs e)
    {
        if (_dataContext.IsModifyMode && (_dataContext.SelectedAddress == null ||
                                         MapService.Address.FormattedAddress ==
                                         _dataContext.SelectedAddress.FormattedAddress)) return;
        MapService.Address = _dataContext.SelectedAddress;
        Map.Children.Clear();
        List<double> coordinates;
        coordinates = await Task.Run(() => MapService.GeocodeAddress());
        if (coordinates != null)
        {
            MakeNewPin(coordinates);
            return;
        }

        MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void RestaurantsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedRestaurant = (Restaurant)RestaurantsTable.SelectedItem;
        if (selectedRestaurant == null) return;
        FillFields(selectedRestaurant);
        DeleteButton.IsEnabled = true;
        NameField.Focus();
        NameField.CaretIndex = selectedRestaurant.Name.Length;
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (!DeleteButton.IsEnabled) return;
        var result = MessageBox.Show("Da li ste sigurni da želite da obrišete označeni restoran?" +
                                     " Nakon brisanja, podaci o restoranu će trajno biti obrisani.", "Potvrda brisanja",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        MessageBoxManager.Yes = "Da";
        MessageBoxManager.No = "Ne";
        if (result == MessageBoxResult.Yes)
        {
            RestaurantService.Delete(_selectedRestaurant);
            if (SuccessSnackbar.MessageQueue is { } messageQueue)
                Task.Factory.StartNew(() => messageQueue.Enqueue("Restoran uspešno obrisan."));
            _restaurants.Remove(_selectedRestaurant);
            ClearFields();
        }
    }

    private void ModifyButton_Click(object sender, RoutedEventArgs e)
    {
        if (!ModifyButton.IsEnabled) return;
        _dataContext.CurrentRestaurant = _selectedRestaurant;
        if (_dataContext.SelectedAddress == null || _dataContext.SelectedAddress.FormattedAddress == "")
        {
            MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (MapService.Address.FormattedAddress != _dataContext.CurrentRestaurant.Address)
        {
            List<double> coordinates = MapService.GeocodeAddress();
            if (coordinates == null)
            {
                MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _dataContext.CurrentRestaurant.Latitude = coordinates[0];
            _dataContext.CurrentRestaurant.Longitude = coordinates[1];
        }

        _dataContext.CurrentRestaurant.IsGlutenFreeFriendly = IsGlutenFreeFriendlyCb.IsChecked == true;
        _dataContext.CurrentRestaurant.IsVeganFriendly = IsVeganFriendlyCb.IsChecked == true;
        _dataContext.CurrentRestaurant.Address = LocationField.Text;
        RestaurantsTable.Items.Refresh();
        RestaurantService.Update(_dataContext.CurrentRestaurant);
        ClearFields();
        NameField.Focus();
        if (SuccessSnackbar.MessageQueue is { } messageQueue)
            Task.Factory.StartNew(() => messageQueue.Enqueue("Restoran uspešno izmenjen."));
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

    private class CustomDataContext : INotifyPropertyChanged
    {
        private Restaurant _currentRestaurant;
        private bool _isModifyMode;
        private Address _selectedAddress;

        public CustomDataContext()
        {
            CurrentRestaurant = new Restaurant();
            SelectedAddress = new Address();
            IsModifyMode = false;
        }

        public bool IsModifyMode
        {
            get => _isModifyMode;
            set
            {
                _isModifyMode = value;
                OnPropertyChanged();
            }
        }

        public Restaurant CurrentRestaurant
        {
            get => _currentRestaurant;
            set
            {
                _currentRestaurant = value;
                OnPropertyChanged();
            }
        }

        public Address SelectedAddress
        {
            get => _selectedAddress;
            set
            {
                _selectedAddress = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    private void AddressFilterField_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        Search();
    }

    private void PlaceCRUD_OnClick(object sender, RoutedEventArgs e)
    {
        var placeWindow = new CRUDPlaceWindow();
        placeWindow.Show();
        Close();
    }

    private void TripCRUD_Click(object sender, RoutedEventArgs e)
    {
        var tripWindow = new CRUDTripWindow();
        tripWindow.Show();
        Close();
    }
    private void Report(object sender, RoutedEventArgs e)
    {
        var reportWindow = new ReportWindow();
        reportWindow.Show();
        Close();
    }
}