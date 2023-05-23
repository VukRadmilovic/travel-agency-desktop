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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Turisticka_Agencija
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ODRADI SAMO PRVI PUT DA VIDIS DA LI TI RADI KAKO TREBA ENTITY!
           /* using (var ctx = new Context())
            {
                var user = new User("Maja", "Varga", "maja.varga@mail.com", "lozinka321");
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }*/
        }
    }
}
