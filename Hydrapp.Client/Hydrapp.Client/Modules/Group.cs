using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class Group: EntityData
    {
        [JsonProperty(PropertyName = "GroupId")]
        public int GroupId { get; set; }
        [JsonProperty(PropertyName = "GroupName")]
        public string GroupName { get; set; }
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

        public Group() { }

        public Group(string groupName, string password)
        {
            this.GroupName = groupName;
            this.Password = password;
        }
    }
}
