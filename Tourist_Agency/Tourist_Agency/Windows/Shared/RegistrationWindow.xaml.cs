using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Agency.Help;
using Tourist_Agency.Models;
using Tourist_Agency.Services;
using Tourist_Agency.Windows.Admin;
using Key = System.Windows.Input.Key;

namespace Tourist_Agency.Windows.Shared;

/// <summary>
///     Interaction logic for Registration.xaml
/// </summary>
public partial class Registration : Window
{
    private void HelpClick(object sender, ExecutedRoutedEventArgs e)
    {
        HelpProvider.ShowHelp("HelpRegister");
    }
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