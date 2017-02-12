using System;
using System.Net.Http.Headers;
using System.Net.Http;

using Windows.UI.Popups;
using BlackJack2.Models;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Runtime.CompilerServices;

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
        //---------- Fonction qui permet d'obtenir la liste des tables ouvertes --------------
        public async void getTables()
         {
            
            using (var client = new HttpClient()) 
             {
                 client.BaseAddress = new Uri("http://demo.comte.re");
                 client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("accept", "application/json");
                 client.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjI5ZjVhOTI2MjZhNjVkYTUzYWM4Y2M2MmExMmMxY2M3MWFlNjJmOWFiM2ZlNWNjNWNkZmEzNWMzZjk0NGZmMjI2Y2NiMjU4YmUzNjBjNTc2In0.eyJhdWQiOiJzeWx2aWVAcHJvamV0Mm1lcmRlLmNvbSIsImp0aSI6IjI5ZjVhOTI2MjZhNjVkYTUzYWM4Y2M2MmExMmMxY2M3MWFlNjJmOWFiM2ZlNWNjNWNkZmEzNWMzZjk0NGZmMjI2Y2NiMjU4YmUzNjBjNTc2IiwiaWF0IjoxNDg2OTMxMDk2LCJuYmYiOjE0ODY5MzEwOTYsImV4cCI6MTUxODQ2NzA5Niwic3ViIjoiNDQiLCJzY29wZXMiOlsiKiJdfQ.Z1hX_h9FT7pPgXmp-hKpRzxhDTDZi368zJ_ltFKbXY_r88Hj2FGj0D2laEmWXdldEfE0Ba-CtbpuVBL62M13eJwVHW30HI-haqvfgs4IYzH7mtIENL_pHhw8Khvr2RD2_bBYOs41tKNnpWah7kw0rQCduI3IwO4Rzdf-hTBMuQ0lESUfdq1OKfSyKfTsguHZmeDoYcZxBBfHItgqEjxbdEmDFCsa3rvMHjtdoSfEe0KdwYSi7UsfX16JfHNEwq2SWXBEzkdTXcFkV8Akhb3mojHiBaywDKGVLBcfAN_Wh5045oQe2Ab1n3Vd0UKcf-nsa6MfIhAXfNK0qVd3f6lnG8LLLFGvVefwtR-seWbbwCpEnr2Z4PuJj5j1Q9kLoXkJ8ojdzKYi3Qr90R0BwMHfjmaXHOkght5QuF8X3TzeYOO7jShvwBhMpvocMrmI7DGhbTLLGbm_fE1xx6J9S7xlaQu8kUTz9KNtW1WpMfpsrCbfBmqSRGdTkiwoYsv3pzs_azETfIeuhPI3MDM7Gr8OWzBwRIECemjW8ZCJDbjcCt5pDTGQFutGvGCOtcL8uE91i6FtinKCDv0cwFMzvsFM60Vp7YpD-p2x_Afx5b2pv_3_vD3nIoer4Lmzm2cHTDNRRnhhH13mHlINavaLFb3UtH1uUnudSbejNZrcCSuGOoQ");
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