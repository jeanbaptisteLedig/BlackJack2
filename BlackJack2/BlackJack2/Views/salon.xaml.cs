using BlackJack2.Models;
using BlackJack2.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using BlackJack2.Models;
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

            
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TableViewModel TableViewModel = new TableViewModel((APIculteur)e.Parameter);
            this.DataContext = TableViewModel;
            TableViewModel.getTables();
            TableViewModel.decoUser();
        }

        private void textBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void signout_Click(object sender, RoutedEventArgs e)
        {


            TableViewModel deco = new TableViewModel();
            this.DataContext = deco;
            deco.decoUser();
        }
    }
}
