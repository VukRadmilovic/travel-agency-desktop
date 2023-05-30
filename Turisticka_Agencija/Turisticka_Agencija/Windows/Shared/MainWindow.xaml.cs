using System.Threading.Tasks;
using System.Windows;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;

namespace Turisticka_Agencija;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = LoginInfo;
    }

    public LoginDTO LoginInfo { get; set; } = new();

    private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        ProgressSpinner.Visibility = Visibility.Visible;
        var isValidLogin = false;
        await Task.Run(() => isValidLogin = UserService.TryLogin(LoginInfo));
        if (!isValidLogin)
        {
            if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                await Task.Factory.StartNew(() => messageQueue.Enqueue("Email ili lozinka nisu tačni."));
        }
        else
        {
            if (CredentialsErrorSnackbar.MessageQueue is { } messageQueue)
                await Task.Factory.StartNew(() => messageQueue.Enqueue("Uspešna prijava."));
        }

        ProgressSpinner.Visibility = Visibility.Hidden;
    }
}