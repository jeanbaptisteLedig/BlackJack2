using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack2.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using Windows.UI.Popups;

namespace BlackJack2.ViewModel
{
    class TableViewModel
    {
        public async void getTables()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");
                client.DefaultRequestHeaders.Accept.Clear();
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
    }
}
