using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using Turisticka_Agencija.Help;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Windows.Shared;

namespace Turisticka_Agencija.Windows.Admin;

/// <summary>
///     Interaction logic for CRUDPlaceWindow.xaml
/// </summary>
public partial class CRUDPlaceWindow : Window
{
    private void HelpClick(object sender, ExecutedRoutedEventArgs e)
    {
        HelpProvider.ShowHelp("HelpCRUDPlace");
    }
    private readonly CustomDataContext _dataContext = new();
    private readonly ObservableCollection<Place> _places = new();
    private ObservableCollection<Place> _searchedPlaces = new();
    private Place _selectedPlace;

    public CRUDPlaceWindow()
    {
        InitializeComponent();
        foreach (var place in PlaceService.GetAll()) _places.Add(place);
        PlacesTable.ItemsSource = _places;
        VirtualizingPanel.SetIsVirtualizing(PlacesTable, true);
        VirtualizingPanel.SetVirtualizationMode(PlacesTable, VirtualizationMode.Recycling);
        DataContext = _dataContext;

        NavigateToCrudTrip.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
        NavigateToCrudRestaurant.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Alt));
        NavigateToCrudAccommodation.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
        ClearFieldsCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
        SearchCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
        SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
        ModifyCommand.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
        DeleteCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
        LogoutCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Alt));
    }

    public static RoutedCommand NavigateToCrudTrip { get; } = new();
    public static RoutedCommand NavigateToCrudRestaurant { get; } = new();
    public static RoutedCommand NavigateToCrudAccommodation { get; } = new();
    public static RoutedCommand LogoutCommand { get; } = new();
    public static RoutedCommand ClearFieldsCommand { get; } = new();
    public static RoutedCommand SaveCommand { get; } = new();
    public static RoutedCommand ModifyCommand { get; } = new();
    public static RoutedCommand DeleteCommand { get; } = new();
    public static RoutedCommand SearchCommand { get; } = new();

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
        _searchedPlaces.Clear();
        var nameField = NameFilterField.Text;
        var addressField = AddressFilterField.Text;

        if (nameField != "" && addressField != null)
        {
            foreach (var place in _places)
                if (place.Name.IndexOf(nameField, StringComparison.OrdinalIgnoreCase) >= 0 &&
                    place.Address.IndexOf(addressField, StringComparison.OrdinalIgnoreCase) >= 0)
                    _searchedPlaces.Add(place);
        }

        else if (nameField == "" && addressField != null)
        {
            foreach (var place in _places)
                if (place.Address.IndexOf(addressField, StringComparison.OrdinalIgnoreCase) >= 0)
                    _searchedPlaces.Add(place);
        }

        else if (nameField != "" && addressField == null)
        {
            foreach (var place in _places)
                if (place.Name.IndexOf(nameField, StringComparison.OrdinalIgnoreCase) >= 0)
                    _searchedPlaces.Add(place);
        }

        else
        {
            _searchedPlaces = new ObservableCollection<Place>(_places);
        }

        PlacesTable.ItemsSource = null;
        PlacesTable.ItemsSource = _searchedPlaces;
    }

    private void ClearFields()
    {
        var dataContext = DataContext as CustomDataContext;
        dataContext.IsModifyMode = false;
        dataContext.CurrentPlace = new Place();
        dataContext.SelectedAddress = new Address();
        MapService.Address = dataContext.SelectedAddress;
        DataContext = dataContext;
        _selectedPlace = null;
        PlacesTable.UnselectAllCells();
        NameField.Text = "";
        LocationField.Text = "";
        InfoLinkField.Text = "";
        DescriptionField.Text = "";

        Map.Children.Clear();
        DeleteButton.IsEnabled = false;
        NameField.Focus();
    }


    private void FillFields(Place selected)
    {
        var dataContext = DataContext as CustomDataContext;
        dataContext.CurrentPlace = selected;
        var copyAddress = new Address();
        copyAddress.FormattedAddress = selected.Address;
        dataContext.SelectedAddress = copyAddress;
        dataContext.IsModifyMode = true;
        DataContext = dataContext;

        NameField.Text = selected.Name;
        LocationField.Text = selected.Address;
        InfoLinkField.Text = selected.InfoLink;
        DescriptionField.Text = selected.Description;


        MakeNewPin(new List<double> { selected.Latitude, selected.Longitude });
        MapService.Address.FormattedAddress = selected.Address;
        _selectedPlace = selected;
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

        var coordinates = MapService.GeocodeAddress();
        if (coordinates == null)
        {
            MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        _dataContext.CurrentPlace.Latitude = coordinates[0];
        _dataContext.CurrentPlace.Longitude = coordinates[1];
        _dataContext.CurrentPlace.Address = _dataContext.SelectedAddress.FormattedAddress;
        PlaceService.Save(_dataContext.CurrentPlace);
        _places.Insert(0, _dataContext.CurrentPlace);
        ClearFields();
        NameField.Focus();
        if (SuccessSnackbar.MessageQueue is { } messageQueue)
            Task.Factory.StartNew(() => messageQueue.Enqueue("Atrakcija uspešno dodata."));
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

    private void PlacesTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedPlace = (Place)PlacesTable.SelectedItem;
        if (selectedPlace == null) return;
        FillFields(selectedPlace);
        DeleteButton.IsEnabled = true;
        NameField.Focus();
        NameField.CaretIndex = selectedPlace.Name.Length;
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (!DeleteButton.IsEnabled) return;
        var result = MessageBox.Show("Da li ste sigurni da želite da obrišete označenu atrakciju?" +
                                     " Nakon brisanja, podaci o atrakciji će trajno biti obrisani.", "Potvrda brisanja",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        MessageBoxManager.Yes = "Da";
        MessageBoxManager.No = "Ne";
        if (result == MessageBoxResult.Yes)
        {
            PlaceService.Delete(_selectedPlace);
            if (SuccessSnackbar.MessageQueue is { } messageQueue)
                Task.Factory.StartNew(() => messageQueue.Enqueue("Atrakcija uspešno obrisana."));
            _places.Remove(_selectedPlace);
            ClearFields();
        }
    }

    private void ModifyButton_Click(object sender, RoutedEventArgs e)
    {
        if (!ModifyButton.IsEnabled) return;
        _dataContext.CurrentPlace = _selectedPlace;
        if (_dataContext.SelectedAddress == null || _dataContext.SelectedAddress.FormattedAddress == "")
        {
            MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (MapService.Address.FormattedAddress != _dataContext.CurrentPlace.Address)
        {
            var coordinates = MapService.GeocodeAddress();
            if (coordinates == null)
            {
                MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _dataContext.CurrentPlace.Latitude = coordinates[0];
            _dataContext.CurrentPlace.Longitude = coordinates[1];
        }

        _dataContext.CurrentPlace.Address = LocationField.Text;
        PlacesTable.Items.Refresh();
        PlaceService.Update(_dataContext.CurrentPlace);
        ClearFields();
        NameField.Focus();
        if (SuccessSnackbar.MessageQueue is { } messageQueue)
            Task.Factory.StartNew(() => messageQueue.Enqueue("Atrakcija uspešno izmenjena."));
    }

    private void NameFilterField_TextChanged(object sender, TextChangedEventArgs e)
    {
        Search();
    }

    private void AddressFilterField_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        Search();
    }

    private void RestaurantCRUD_OnClick(object sender, RoutedEventArgs e)
    {
        var restaurantWindow = new CRUDRestaurantWindow();
        restaurantWindow.Show();
        Close();
    }

    private class CustomDataContext : INotifyPropertyChanged
    {
        private Place _currentPlace;
        private bool _isModifyMode;
        private Address _selectedAddress;

        public CustomDataContext()
        {
            CurrentPlace = new Place();
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

        public Place CurrentPlace
        {
            get => _currentPlace;
            set
            {
                _currentPlace = value;
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

    private void TripCRUD_OnClick(object sender, RoutedEventArgs e)
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