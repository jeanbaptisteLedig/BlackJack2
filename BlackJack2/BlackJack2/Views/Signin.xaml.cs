﻿using System;
using System.Security.Cryptography;
using System.Text;

using BlackJack2.ViewModel;
using BlackJack2.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlackJack2.Views
{
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string email = mailCon.Text;
            string password = passwordCon.Password;
            string secret = GetMD5(password);

            User user = new User(email, password, secret);

            PersonViewModel vm = new PersonViewModel();
            vm.conUser(user);
        }

        //--------- Fonction de hashage -----------
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

        private void textBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
