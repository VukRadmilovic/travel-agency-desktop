using System;
using System.Collections.Generic;
using System.IO;
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

namespace Turisticka_Agencija.Help
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {
        public HelpViewer(string key)
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();
            string path = String.Format("C:\\Users\\Dracooya\\Desktop\\Fax\\3. godina\\6. semestar\\Interakcija čovek računar\\Veliki projekat\\hci-tim14-agencija\\Turisticka_Agencija\\Turisticka_Agencija\\Help\\{0}.htm", key);
            if (!File.Exists(path))
            {
                key = "error";
            }
            Uri u = new Uri(String.Format("file://C:\\Users\\Dracooya\\Desktop\\Fax\\3. godina\\6. semestar\\Interakcija čovek računar\\Veliki projekat\\hci-tim14-agencija\\Turisticka_Agencija\\Turisticka_Agencija\\Help\\{0}.htm", key));
            wbHelp.Navigate(u);
        }

        private void BrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((wbHelp != null) && (wbHelp.CanGoBack));
        }

        private void BrowseBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((wbHelp != null) && (wbHelp.CanGoForward));
        }

        private void BrowseForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoForward();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void wbHelp_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }


    }
}
