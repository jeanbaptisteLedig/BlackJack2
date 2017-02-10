using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;
using Windows.UI.Popups;
using BlackJack2.Models;
using System.Net.Http.Headers;
using BlackJack2.Views;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;

namespace BlackJack2.ViewModel
{
    class PersonViewModel
    {
        public object Frame { get; private set; }
        
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
                    //On récupère la réponse JSON de l'API, puis on la parse pour pouvoir lire dedans
                    var res = await responseAPI.Content.ReadAsStringAsync();
                    var objectJson = JObject.Parse(res);
                    var tokenObject = objectJson["tokens"];
                    var userObject = objectJson["user"];

                    //On stocke tous les résultats obtenus dans le JSON
                    user.token = tokenObject["access_token"].ToString();
                    user.firstname = userObject["firstname"].ToString();
                    user.lastname = userObject["lastname"].ToString();
                    user.email = userObject["email"].ToString();
                    user.stack = int.Parse(userObject["stack"].ToString());

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
