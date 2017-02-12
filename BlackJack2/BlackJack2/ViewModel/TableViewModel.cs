using System;
using System.Net.Http.Headers;
using System.Net.Http;

using Windows.UI.Popups;
using BlackJack2.Models;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Text;
using System.Windows.Input;

namespace BlackJack2.ViewModel
{
    class TableViewModel
     {
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
        public TableViewModel(APIculteur Api)
        {
            this._api = Api;
            getTables();
        }

        public TableViewModel()
        {
        }

        //---------- Fonction qui permet d'obtenir la liste des tables ouvertes --------------
        public async void getTables()
         {
            
            using (var client = new HttpClient()) 
             {
                 client.BaseAddress = new Uri("http://demo.comte.re");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this._api.token.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage();



                HttpResponseMessage response = await client.GetAsync("/api/table/opened");

                if (response.IsSuccessStatusCode) 
                 { 
                     string res = await response.Content.ReadAsStringAsync(); 
                     var dialog = new MessageDialog("get tableok ok");
                     await dialog.ShowAsync(); 
                 }
                 else 
                 {
                     string res = await response.Content.ReadAsStringAsync(); 
                     var dialog = new MessageDialog("pas ok ", res); 
                     await dialog.ShowAsync(); 
                 } 
             } 
         }
        private RelayCommand logout;
        public ICommand LogoutCommand
        {
            get
            {
                
                {
                    logout = logout ?? (logout = new RelayCommand(p => { decoUser(); }));
                }
                return logout;
            }
        }

        // deconnexion de l'utilisateur
        
        public async void decoUser()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re/");

                var json = JsonConvert.SerializeObject(this.Api.user.email);
                json = "{\"email\":" + json + "}";
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/api/auth/logout", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    var dialog = new MessageDialog("Vous êtes déconnecté");
                    await dialog.ShowAsync();

                    currentFrame.Navigate(typeof(MainPage));
                }
                else
                {
                    string res = await response.Content.ReadAsStringAsync();
                    var dialog = new MessageDialog("pas ok ", json);
                    await dialog.ShowAsync();
                }
            }
        }
    } 
 }