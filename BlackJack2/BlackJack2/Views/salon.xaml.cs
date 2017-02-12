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
    public sealed partial class Salon : Page
    {
        public Salon()
        {
            this.InitializeComponent();

            TableViewModel listTable = new TableViewModel();
            listTable.getTables();
        }

        private void textBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void signout_Click(object sender, RoutedEventArgs e)
        {
            string email = "jblebgdu13@projet2merde.com";

            User userSignout = new User(email);

            TableViewModel signout = new TableViewModel();
            signout.decoUser(userSignout);
        }
    }
}
