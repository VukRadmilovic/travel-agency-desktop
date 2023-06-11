using System;
using System.Collections.Generic;
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

        public CRUDTripWindow()
        {
            InitializeComponent();

            NavigateToCrudRestaurant.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Alt));
            NavigateToCrudAccommodation.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            NavigateToCrudPlace.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
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
    }
}
