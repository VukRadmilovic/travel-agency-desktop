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
using Turisticka_Agencija.Help;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Utils;

namespace Turisticka_Agencija.Windows.Shared
{
    /// <summary>
    /// Interaction logic for ViewBoughtTripsWindow.xaml
    /// </summary>
    public partial class ViewBoughtTripsWindow : Window
    {
        private readonly ObservableCollection<TripUser> _trips = new();
        private void HelpClick(object sender, ExecutedRoutedEventArgs e)
        {
            HelpProvider.ShowHelp("HelpBought");
        }
        public ViewBoughtTripsWindow()
        {
            InitializeComponent();
            foreach (var trip in TripService.GetAll())
            {
                bool isBought = false;
                bool isReserved = false;
                if(TripBoughtOrReservedByUserService.IsBought(trip, UserService.LoggedUser))
                {
                    isBought = true;
                }
                if (TripBoughtOrReservedByUserService.IsReserved(trip, UserService.LoggedUser))
                {
                    isReserved = true;
                }
                if (isBought || isReserved)
                {
                    _trips.Add(new TripUser(trip, isBought, isReserved));
                }
            }

            TripsTable.ItemsSource = _trips;
            VirtualizingPanel.SetIsVirtualizing(TripsTable, true);
            VirtualizingPanel.SetVirtualizationMode(TripsTable, VirtualizationMode.Recycling);
            BuildMenuItems();
        }
        private void BuildMenuItems()
        {
            Menu.Items.Clear();

            if (!UserService.IsLoggedIn)
            {
                var loginMenuItem = new MenuItem
                {
                    Header = "Prijava",
                    Name = "loginMenuItem",
                };
                loginMenuItem.Click += loginMenuItem_Click;
                Menu.Items.Add(loginMenuItem);

                var registerMenuItem = new MenuItem
                {
                    Header = "Registracija",
                    Name = "registerMenuItem",
                };
                registerMenuItem.Click += registerMenuItem_Click;
                Menu.Items.Add(registerMenuItem);
            }
            else
            {
                var tripMenuItem = new MenuItem
                {
                    Header = "Sva Putovanja",
                    Name = "tripMenuItem",
                };
                tripMenuItem.Click += tripMenuItem_Click;
                Menu.Items.Add(tripMenuItem);

                var logoutMenuItem = new MenuItem
                {
                    Header = "Odjava",
                    Name = "logoutMenuItem",
                };
                logoutMenuItem.Click += logoutMenuItem_Click;
                Menu.Items.Add(logoutMenuItem);
            }

            var helpMenuItem = new MenuItem
            {
                Header = "Pomoć",
                Name = "helpMenuItem",
            };
            helpMenuItem.Click += helpMenuItem_Click;
            Menu.Items.Add(helpMenuItem);
        }

        private void loginMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new();
            window.Show();
            Close();
        }

        private void registerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Registration window = new();
            window.Show();
            Close();
        }

        private void logoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            UserService.LoggedUser = null;
            UserService.IsLoggedIn = false;

            ViewAllTripsWindow window = new();
            window.Show();
            Close();
        }

        private void tripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ViewAllTripsWindow window = new();
            window.Show();
            Close();
        }

        private void helpMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            ViewOneTripWindow window = new(((TripUser)TripsTable.SelectedItem).Trip);
            Hide();
            window.ShowDialog();
            Show();
        }

        private void TripsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTrip = ((TripUser)TripsTable.SelectedItem).Trip;
            if (selectedTrip == null)
            {
                ViewButton.IsEnabled = false;
                return;
            }

            ViewButton.IsEnabled = true;
        }

        private void TripsTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTrip = (Trip)TripsTable.SelectedItem;
            if (selectedTrip == null)
            {
                return;
            }

            ViewOneTripWindow window = new(selectedTrip);
            Hide();
            window.ShowDialog();
            Show();
        }
    }
}
