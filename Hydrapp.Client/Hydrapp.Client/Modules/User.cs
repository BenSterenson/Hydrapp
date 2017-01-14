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
        [JsonProperty(PropertyName = "UserId")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "UserName")]
        public string userName { get; set; }
        [JsonProperty(PropertyName = "Password")]
        public string password { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string email { get; set; }
        [JsonProperty(PropertyName = "Height")]
        public double height { get; set; }
        [JsonProperty(PropertyName = "Weight")]
        public double weight { get; set; }

        public User() { }

        public User(string username, string password, string email, double height, double weight)
        {
            this.userName = username;
            this.password = password;
            this.email = email;
            this.height = height;
            this.weight = weight;
        }
    }
}
