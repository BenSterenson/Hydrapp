using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class TestItem : EntityData
    {
        [JsonProperty(PropertyName = "name")]
        public String name { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime date { get; set; }

        public TestItem(){}

        public TestItem(String username, DateTime v)
        {
            this.name = username;
            this.date = v;
        }
    }
}
