using BlackJack2.Models;
using BlackJack2.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlackJack2.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Signup : Page
    {
        public Signup()
        {
            this.InitializeComponent();
        }

        private void returnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string email = mail.Text;
            string username = pseudo.Text;
            string lastname = prenom.Text;
            string firstname = nom.Text;
            string password = this.password.Password;

            User u = new User(username, firstname, lastname, email, password);

            PersonViewModel vm = new PersonViewModel();
            vm.addNewUser(u);
        }
    }
}
