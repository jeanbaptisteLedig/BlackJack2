using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack2.Models
{
    
        public class User
        {
            public string username { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string secret { get; set; }
            public string token { get; set; }
            public int stack { get; set; }

        public User(string username, string firstname, string lastname, string email, string password)
            {
                this.username = username;
                this.firstname = firstname;
                this.lastname = lastname;
                this.email = email;
                this.password = password;
            }
      
        public User(string email, string password, string secret)
            {
            this.email = email;
            this.password = password;
            this.secret = secret;
            }
        }

}