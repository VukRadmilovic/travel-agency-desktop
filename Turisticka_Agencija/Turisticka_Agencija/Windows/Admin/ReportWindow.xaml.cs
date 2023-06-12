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
using Turisticka_Agencija.Utils;
using Turisticka_Agencija.Windows.Shared;

namespace Turisticka_Agencija.Windows.Admin
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private ObservableCollection<Report> _report = new();
        public ReportWindow()
        {
            InitializeComponent();
            YearFilterField.SelectedIndex = 0;
            MonthFilterField.SelectedIndex = 0;
        }

        private void TableFilterField_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _report.Clear();
            foreach (var trip in TripService.GetAll())
            {
                int year = 1;
                int month = 1;
                if (YearFilterField.SelectedItem != null)
                {
                    year = int.Parse(((ComboBoxItem)YearFilterField.SelectedItem).Content.ToString());
                }
                if (MonthFilterField.SelectedItem != null)
                {
                    month = int.Parse(((ComboBoxItem)MonthFilterField.SelectedItem).Content.ToString());
                }
                _report.Add(new Report(trip, TripBoughtOrReservedByUserService.CountBought(trip, new DateTime(year, month, 1))));
            }

            TripsTable.ItemsSource = _report;
            VirtualizingPanel.SetIsVirtualizing(TripsTable, true);
            VirtualizingPanel.SetVirtualizationMode(TripsTable, VirtualizationMode.Recycling);
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
