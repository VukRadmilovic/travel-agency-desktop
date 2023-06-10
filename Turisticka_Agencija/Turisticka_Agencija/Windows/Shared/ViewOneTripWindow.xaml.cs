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
using Turisticka_Agencija.Models;

namespace Turisticka_Agencija.Windows.Shared
{
    /// <summary>
    /// Interaction logic for ViewOneTripWindow.xaml
    /// </summary>
    public partial class ViewOneTripWindow : Window
    {
        public ViewOneTripWindow()
        {
            InitializeComponent();
        }

        public ViewOneTripWindow(Trip trip)
        {
            InitializeComponent();

            MessageBox.Show(trip.ToString());
        }
    }
}
