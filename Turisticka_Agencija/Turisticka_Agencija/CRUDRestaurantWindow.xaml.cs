using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;
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

namespace Turisticka_Agencija
{
    /// <summary>
    /// Interaction logic for CRUDRestaurant.xaml
    /// </summary>
    public partial class CRUDRestaurantWindow : Window
    {
        private string suggestion = "";
        private string inputAddress = "";
        private ObservableCollection<Restaurant> restaurants = new();
        public CRUDRestaurantWindow()
        {
            InitializeComponent();
            foreach(var restaurant in RestaurantService.GetAll()) restaurants.Add(restaurant);
            RestaurantsTable.ItemsSource = restaurants;
        }
    }
}
