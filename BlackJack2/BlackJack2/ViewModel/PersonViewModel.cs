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

namespace BlackJack2.ViewModel
{
    class PersonViewModel
    {
        public object Frame { get; private set; }

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

                ////    var dialog = new MessageDialog(json);
                ////    await dialog.ShowAsync();

            
            }
        }
        public async void conUser(User user)

        {
            
            using (var client = new HttpClient())

            {

                client.BaseAddress = new Uri("http://demo.comte.re");
                var json = JsonConvert.SerializeObject(user);
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/api/auth/login", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    
                    var dialog = new MessageDialog("ok");
                    await dialog.ShowAsync();
                   
                }
                else
                {
                    var dialog = new MessageDialog("pas ok ",json);
                    await dialog.ShowAsync();
                }

            }


        }
    }
}
