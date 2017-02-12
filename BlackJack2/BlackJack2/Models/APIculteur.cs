using Newtonsoft.Json;

namespace BlackJack2.Models
{
    //Classe APICulteur
    class APIculteur
    {
        [JsonProperty("user")]
        public User user { get; set; }
        [JsonProperty("tokens")]
        public Token token { get; set; }

        public APIculteur()
        {
            this.user = new User();
            this.token = token;
        }
    }
}
