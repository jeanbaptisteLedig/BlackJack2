using System;
using System.Net.Http.Headers;
using System.Net.Http;

using Windows.UI.Popups;
using Newtonsoft.Json;
using System.Text;
using BlackJack2.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace BlackJack2.ViewModel
{
    class TableViewModel
     {
        Frame currentFrame { get { return Window.Current.Content as Frame; } }
        //---------- Fonction qui permet d'obtenir la liste des tables ouvertes --------------
        public async void getTables()
         {
             using (var client = new HttpClient()) 
             {
                 client.BaseAddress = new Uri("http://demo.comte.re");
                 client.DefaultRequestHeaders.Accept.Clear();
                 //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                 client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 HttpResponseMessage response = await client.GetAsync("/api/table/opened");
                 
                 if (response.IsSuccessStatusCode) 
                 { 
                     string res = await response.Content.ReadAsStringAsync(); 
                     var dialog = new MessageDialog("ok", res);
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

        // ----------- Fonction de déconnexion -----------
        public async void decoUser(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re/");

                var json = JsonConvert.SerializeObject(user.email);
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