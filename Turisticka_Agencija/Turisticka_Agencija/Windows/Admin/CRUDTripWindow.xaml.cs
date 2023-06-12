using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Turisticka_Agencija.Help;
using Turisticka_Agencija.Windows.Shared;

namespace Turisticka_Agencija.Windows.Admin
{
    /// <summary>
    /// Interaction logic for CRUDTripWindow.xaml
    /// </summary>
    public partial class CRUDTripWindow : Window
    {
        private void HelpClick(object sender, ExecutedRoutedEventArgs e)
        {
            HelpProvider.ShowHelp("HelpCRUDTrip");
        }
        public static RoutedCommand NavigateToCrudRestaurant { get; } = new();
        public static RoutedCommand NavigateToCrudAccommodation { get; } = new();
        public static RoutedCommand NavigateToCrudPlace { get; } = new();
        public static RoutedCommand LogoutCommand { get; } = new();
        public static RoutedCommand ClearFieldsCommand { get; } = new();
        public static RoutedCommand SaveCommand { get; } = new();
        public static RoutedCommand ModifyCommand { get; } = new();
        public static RoutedCommand DeleteCommand { get; } = new();

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

        private void TripsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            MessageBox.Show("Cleared");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Saved");
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Modified");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Deleted");
        }
        private void Report(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow();
            reportWindow.Show();
            Close();
        }
    }
}
