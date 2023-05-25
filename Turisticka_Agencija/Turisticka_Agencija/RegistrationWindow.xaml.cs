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
using System.Windows.Resources;
using System.Windows.Shapes;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;

namespace Turisticka_Agencija
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public User userInfo { get; set; } = new();

        public Registration()
        {
            InitializeComponent();
            DataContext = userInfo;
        }

        private async void RegisterButton_OnClickButton_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressSpinner.Visibility = Visibility.Visible;
            var isValidRegistration = false;
            await Task.Run(() => isValidRegistration = UserService.TryRegistration(userInfo));
            if (!isValidRegistration)
            {
                if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                {
                    await Task.Factory.StartNew(() => messageQueue.Enqueue("Uneti email je zauzet."));
                }

            }
            else
            {
                if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                {
                    await Task.Factory.StartNew(() => messageQueue.Enqueue("Uspešna registracija."));
                }
            }

            ProgressSpinner.Visibility = Visibility.Hidden;
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PasswordMask.Visibility == Visibility.Collapsed)
            {
                PasswordField.Visibility = System.Windows.Visibility.Collapsed;
                PasswordMask.Visibility = System.Windows.Visibility.Visible;

                PasswordMask.Focus();
            }
            else
            {
                PasswordField.Visibility = System.Windows.Visibility.Visible;
                PasswordMask.Visibility = System.Windows.Visibility.Collapsed;

                PasswordField.Focus();
            }
        }
    }
}
