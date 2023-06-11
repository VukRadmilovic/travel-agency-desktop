using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Windows.Admin;
using Turisticka_Agencija.Windows.Shared;
using Key = System.Windows.Input.Key;

namespace Turisticka_Agencija;

/// <summary>
///     Interaction logic for Registration.xaml
/// </summary>
public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
        DataContext = userInfo;
    }

    public User userInfo { get; set; } = new();

    private async void RegisterButton_OnClickButton_OnClick(object sender, RoutedEventArgs e)
    {
        await Register();
    }

    private async Task Register()
    {
        ProgressSpinner.Visibility = Visibility.Visible;
        var isValidRegistration = false;
        User user = null;
        await Task.Run(() => (isValidRegistration, user) = UserService.TryRegistration(userInfo));
        if (!isValidRegistration)
        {
            if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                await Task.Factory.StartNew(() => messageQueue.Enqueue("Uneti email je zauzet."));
        }
        else
        {
            if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                await Task.Factory.StartNew(() => messageQueue.Enqueue("Uspešna registracija."));
            if (user.Type == UserType.Agent)
            {
                CRUDPlaceWindow window = new();
                window.Show();
                Close();
            }
            else
            {
                ViewAllTripsWindow window = new();
                window.Show();
                Close();
            }
        }

        ProgressSpinner.Visibility = Visibility.Hidden;
    }

    private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (PasswordMask.Visibility == Visibility.Collapsed)
        {
            PasswordField.Visibility = Visibility.Collapsed;
            PasswordMask.Visibility = Visibility.Visible;

            PasswordMask.Focus();
            PasswordMask.CaretIndex = userInfo.Password.Length;
        }
        else
        {
            PasswordField.Visibility = Visibility.Visible;
            PasswordMask.Visibility = Visibility.Collapsed;

            PasswordField.Focus();
        }
    }

    private void PackIcon_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) PackIcon_MouseDown(sender, null);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Register();
        }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (!UserService.IsLoggedIn)
        {
            var window = new ViewAllTripsWindow();
            window.Show();
        }
    }
}