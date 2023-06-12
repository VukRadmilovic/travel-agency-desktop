using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Turisticka_Agencija.Help;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Windows.Admin;

namespace Turisticka_Agencija.Windows.Shared;

/// <summary>
///     Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = LoginInfo;
    }

    public LoginDTO LoginInfo { get; set; } = new();

    private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        await Login();
    }

    private async Task Login()
    {
        ProgressSpinner.Visibility = Visibility.Visible;
        var isValidLogin = false;
        User user = null;
        await Task.Run(() => (isValidLogin, user) = UserService.TryLogin(LoginInfo));
        if (!isValidLogin)
        {
            if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                await Task.Factory.StartNew(() => messageQueue.Enqueue("Email ili lozinka nisu tačni."));
        }
        else
        {
            if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
            {
                await Task.Factory.StartNew(() => messageQueue.Enqueue("Uspešna prijava."));
                if (user.Type == UserType.Agent)
                {
                    CRUDTripWindow window = new();
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
        }

        ProgressSpinner.Visibility = Visibility.Hidden;
    }

    private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Login();
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
    private void HelpClick(object sender, ExecutedRoutedEventArgs e)
    {
        HelpProvider.ShowHelp("HelpLogin");
    }
}