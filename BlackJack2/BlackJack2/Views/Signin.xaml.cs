using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Security.Cryptography;
using System.Text;
using BlackJack2.ViewModel;
using BlackJack2.Models;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlackJack2.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Signin : Page
    {
        public Signin()
        {
            this.InitializeComponent();
        }

        private void returnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void textBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string email = mailCon.Text;
            string password = passwordCon.Text;

            string secret = GetMD5(password);

            User user = new User(email, password, secret);


            PersonViewModel vm = new PersonViewModel();

            vm.conUser(user);
            


        }
        public string GetMD5(string password)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(password);

            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {
                str.Append(hash[i].ToString("x2"));
            }
            var valueBytes = Encoding.UTF8.GetBytes(str.ToString());
            return Convert.ToBase64String(valueBytes);
        }
    }
}
