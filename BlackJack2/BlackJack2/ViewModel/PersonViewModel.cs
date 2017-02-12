using System;
using System.Text;
using System.Net.Http;

using BlackJack2.Models;
using BlackJack2.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlackJack2.ViewModel
{
    class PersonViewModel
    {
        //public object Frame { get; private set; }
        Frame currentFrame { get { return Window.Current.Content as Frame; } }

        private APIculteur _api;
        

        public APIculteur Api
        {
            get { return _api; }
            set
            {
                SetProperty<APIculteur>(ref this._api, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    


        // ------------ Fonction d'ajout d'un utilisateur ---------------
        public async void addNewUser(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");

                var json = JsonConvert.SerializeObject(new { user = user });
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/api/auth/register", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("ok",json);
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("pas ok ",json);
                    await dialog.ShowAsync();
                }
            }
        }
        // ----------- Fonction de connexion de l'utilisateur --> retourne un JSON qui contient le token de session ----------
        public async void conUser(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");
                var json = JsonConvert.SerializeObject(user);
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseAPI = await client.PostAsync("/api/auth/login", itemJson);
                if (responseAPI.IsSuccessStatusCode)
                {
                    
                    String _response = responseAPI.Content.ReadAsStringAsync().Result;
                    this.Api = new APIculteur();
                    this.Api = JsonConvert.DeserializeObject<APIculteur>(_response);
                    Debug.WriteLine(_response);
                    Debug.WriteLine(this.Api.user.stack);
                    currentFrame.Navigate(typeof(Salon), this.Api);
                    var dialog = new MessageDialog("Vous êtes connecté");
                   
                    
                    await dialog.ShowAsync();
                
                }
                else
                {
                    var res = await responseAPI.Content.ReadAsStringAsync();
                    var dialog = new MessageDialog("Connexion refuse", res);
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
