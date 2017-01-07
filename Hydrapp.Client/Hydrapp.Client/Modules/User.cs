using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class User: EntityData
    {
        [JsonProperty(PropertyName = "username")]
        public string userName { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string password { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string email { get; set; }
        [JsonProperty(PropertyName = "bmi")]
        public double bmi { get; set; }

        public User() { }

        public User(string username, string password, string email, double bmi)
        {
            userName = userName;
            this.password = password;
            this.email = email;
            this.bmi = bmi;
        }
    }
}
