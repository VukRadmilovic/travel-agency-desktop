using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;

namespace Turisticka_Agencija
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LoginDTO LoginInfo { get; set; } = new();
        private readonly UserService _userService = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = LoginInfo;
        }

        private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressSpinner.Visibility = Visibility.Visible;
            var isValidLogin = false;
            await Task.Run(() => isValidLogin = _userService.TryLogin(LoginInfo));
            if (!isValidLogin)
            {
                if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                {
                    await Task.Factory.StartNew(() => messageQueue.Enqueue("Email ili lozinka nisu tačni."));
                }

            }
            else
            {
                if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                {
                    await Task.Factory.StartNew(() => messageQueue.Enqueue("Uspešna prijava."));
                }
            }
            ProgressSpinner.Visibility = Visibility.Hidden;
        }
    }
}
