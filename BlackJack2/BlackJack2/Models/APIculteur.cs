using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack2.Models
{
    class APIculteur
    {
        [JsonProperty("user")]
        public User user { get; set; }
        [JsonProperty("tokens")]
        public string token { get; set; }

        public APIculteur()
        {
            this.user = new User();
            this.token = token;

        }
    }
}
