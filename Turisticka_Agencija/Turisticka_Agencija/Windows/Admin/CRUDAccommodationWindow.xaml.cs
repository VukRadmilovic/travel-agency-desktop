using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Windows.Shared;

namespace Turisticka_Agencija.Windows.Admin
{
    /// <summary>
    /// Interaction logic for CRUDAccommodationWindow.xaml
    /// </summary>
    public partial class CRUDAccommodationWindow : Window
    {
        private readonly ObservableCollection<Accommodation> _accommodations = new();
        private ObservableCollection<Accommodation> _searchedAccommodations = new();
        private Accommodation _selectedAccommodation;
        private readonly CustomDataContext _dataContext = new();
        public static RoutedCommand NavigateToCrudRestaurant { get; } = new();
        public static RoutedCommand NavigateToCrudAccommodation { get; } = new();
        public static RoutedCommand NavigateToCrudPlace { get; } = new();
        public static RoutedCommand NavigateToCrudTrip { get; } = new();
        public static RoutedCommand LogoutCommand { get; } = new();
        public static RoutedCommand ClearFieldsCommand { get; } = new();
        public static RoutedCommand SaveCommand { get; } = new();
        public static RoutedCommand ModifyCommand { get; } = new();
        public static RoutedCommand DeleteCommand { get; } = new();
        public static RoutedCommand SearchCommand { get; } = new();

        public CRUDAccommodationWindow()
        {
            InitializeComponent();
            foreach (var accommodation in AccommodationService.GetAll()) _accommodations.Add(accommodation);
            AccommodationsTable.ItemsSource = _accommodations;
            VirtualizingPanel.SetIsVirtualizing(AccommodationsTable, true);
            VirtualizingPanel.SetVirtualizationMode(AccommodationsTable, VirtualizationMode.Recycling);
            DataContext = _dataContext;

            NavigateToCrudRestaurant.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Alt));
            NavigateToCrudPlace.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
            NavigateToCrudTrip.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
            ClearFieldsCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            ModifyCommand.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            DeleteCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            LogoutCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Alt));
            SearchCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
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

        private void RestaurantCRUD_OnClick(object sender, RoutedEventArgs e)
        {
            var restaurantsWindow = new CRUDRestaurantWindow();
            restaurantsWindow.Show();
            Close();
        }

        private void ClearFieldsButton_OnClick(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void Search()
        {
            _searchedAccommodations.Clear();
            var nameField = NameFilterField.Text;
            var addressField = AddressFilterField.Text;
            string starCountStr;
            ComboBoxItem selectedStarCount = (ComboBoxItem)StartCountFilterField.SelectedItem;
            if (selectedStarCount == null) starCountStr = "...";
            else starCountStr= selectedStarCount.Content.ToString();
            int? starCount;
            if(starCountStr == "...") starCount = null;
            else starCount = int.Parse(starCountStr);

            if (nameField != "" && starCount != null)
            {
                foreach (var accommodation in _accommodations)
                {
                    if (accommodation.Name.IndexOf(nameField, StringComparison.OrdinalIgnoreCase) >= 0 &&
                        accommodation.StarCount == starCount)
                        _searchedAccommodations.Add(accommodation);
                }
            }

            else if (nameField == "" && starCount != null)
            {
                foreach (var accommodation in _accommodations)
                {
                    if (accommodation.StarCount == starCount)
                        _searchedAccommodations.Add(accommodation);
                }
            }

            else if (nameField != "" && starCount == null)
            {
                foreach (var accommodation in _accommodations)
                {
                    if (accommodation.Name.IndexOf(nameField, StringComparison.OrdinalIgnoreCase) >= 0)
                        _searchedAccommodations.Add(accommodation);
                }
            }
            else
            {
                _searchedAccommodations = new ObservableCollection<Accommodation>(_accommodations);
            }

            ObservableCollection<Accommodation> finalFilter = new();
            if (addressField != "")
            {
                foreach (var accommodation in _searchedAccommodations)
                {
                    if (accommodation.Address.IndexOf(addressField, StringComparison.OrdinalIgnoreCase) >= 0)
                        finalFilter.Add(accommodation);
                }
            }
            else finalFilter = _searchedAccommodations;

            AccommodationsTable.ItemsSource = null;
            AccommodationsTable.ItemsSource = finalFilter;
        }

        private void ClearFields()
        {
            var dataContext = DataContext as CustomDataContext;
            dataContext.IsModifyMode = false;
            dataContext.CurrentAccommodation = new Accommodation();
            dataContext.SelectedAddress = new Address();
            MapService.Address = dataContext.SelectedAddress;
            DataContext = dataContext;
            _selectedAccommodation = null;
            AccommodationsTable.UnselectAllCells();
            NameField.Text = "";
            LocationField.Text = "";
            InfoLinkField.Text = "";
            DescriptionField.Text = "";
            StarCountBar.Value = 0;
            Map.Children.Clear();
            DeleteButton.IsEnabled = false;
            NameField.Focus();
        }


        private void FillFields(Accommodation selected)
        {
            var dataContext = DataContext as CustomDataContext;
            dataContext.CurrentAccommodation = selected;
            Address copyAddress = new Address();
            copyAddress.FormattedAddress = selected.Address;
            dataContext.SelectedAddress = copyAddress;
            dataContext.IsModifyMode = true;
            DataContext = dataContext;

            NameField.Text = selected.Name;
            LocationField.Text = selected.Address;
            InfoLinkField.Text = selected.InfoLink;
            DescriptionField.Text = selected.Description;
            if (selected.StarCount == null) StarCountBar.Value = 0;
            else StarCountBar.Value = (double)selected.StarCount;

            MakeNewPin(new List<double> { selected.Latitude, selected.Longitude });
            MapService.Address.FormattedAddress = selected.Address;
            _selectedAccommodation = selected;
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

            _dataContext.CurrentAccommodation.Latitude = coordinates[0];
            _dataContext.CurrentAccommodation.Longitude = coordinates[1];
            _dataContext.CurrentAccommodation.Address = _dataContext.SelectedAddress.FormattedAddress;
            if (StarCountBar.Value == 0) _dataContext.CurrentAccommodation.StarCount = null;
            else _dataContext.CurrentAccommodation.StarCount = (int)StarCountBar.Value;
                AccommodationService.Save(_dataContext.CurrentAccommodation);
            _accommodations.Insert(0, _dataContext.CurrentAccommodation);
            ClearFields();
            NameField.Focus();
            if (SuccessSnackbar.MessageQueue is { } messageQueue)
                Task.Factory.StartNew(() => messageQueue.Enqueue("Smeštaj uspešno dodat."));
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

        private void AccommodationsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAccommodation = (Accommodation)AccommodationsTable.SelectedItem;
            if (selectedAccommodation == null) return;
            FillFields(selectedAccommodation);
            DeleteButton.IsEnabled = true;
            NameField.Focus();
            NameField.CaretIndex = selectedAccommodation.Name.Length;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DeleteButton.IsEnabled) return;
            var result = MessageBox.Show("Da li ste sigurni da želite da obrišete označeni smeštaj?" +
                                         " Nakon brisanja, podaci o smeštaju će trajno biti obrisani.", "Potvrda brisanja",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            MessageBoxManager.Yes = "Da";
            MessageBoxManager.No = "Ne";
            if (result == MessageBoxResult.Yes)
            {
                AccommodationService.Delete(_selectedAccommodation);
                if (SuccessSnackbar.MessageQueue is { } messageQueue)
                    Task.Factory.StartNew(() => messageQueue.Enqueue("Smeštaj uspešno obrisan."));
                _accommodations.Remove(_selectedAccommodation);
                ClearFields();
            }
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ModifyButton.IsEnabled) return;
            _dataContext.CurrentAccommodation = _selectedAccommodation;
            if (_dataContext.SelectedAddress == null || _dataContext.SelectedAddress.FormattedAddress == "")
            {
                MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MapService.Address.FormattedAddress != _dataContext.CurrentAccommodation.Address)
            {
                List<double> coordinates = MapService.GeocodeAddress();
                if (coordinates == null)
                {
                    MessageBox.Show("Nepostojeća adresa.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _dataContext.CurrentAccommodation.Latitude = coordinates[0];
                _dataContext.CurrentAccommodation.Longitude = coordinates[1];
            }

            _dataContext.CurrentAccommodation.Address = LocationField.Text;
            if (StarCountBar.Value == 0) _dataContext.CurrentAccommodation.StarCount = null;
            else _dataContext.CurrentAccommodation.StarCount = (int)StarCountBar.Value;
            AccommodationsTable.Items.Refresh();
            AccommodationService.Update(_dataContext.CurrentAccommodation);
            ClearFields();
            NameField.Focus();
            if (SuccessSnackbar.MessageQueue is { } messageQueue)
                Task.Factory.StartNew(() => messageQueue.Enqueue("Smeštaj uspešno izmenjen."));
        }

        private void NameFilterField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private class CustomDataContext : INotifyPropertyChanged
        {
            private Accommodation _currentAccommodation;
            private bool _isModifyMode;
            private Address _selectedAddress;

            public CustomDataContext()
            {
                CurrentAccommodation = new Accommodation();
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

            public Accommodation CurrentAccommodation
            {
                get => _currentAccommodation;
                set
                {
                    _currentAccommodation = value;
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

        private void StartCountFilterField_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
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
            var window = new CRUDTripWindow();
            window.Show();
            Close();
        }
    }
}
