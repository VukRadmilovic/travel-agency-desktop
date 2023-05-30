using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;

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
        ProgressSpinner.Visibility = Visibility.Visible;
        var isValidRegistration = false;
        await Task.Run(() => isValidRegistration = UserService.TryRegistration(userInfo));
        if (!isValidRegistration)
        {
            if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                await Task.Factory.StartNew(() => messageQueue.Enqueue("Uneti email je zauzet."));
        }
        else
        {
            if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                await Task.Factory.StartNew(() => messageQueue.Enqueue("Uspešna registracija."));
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
}