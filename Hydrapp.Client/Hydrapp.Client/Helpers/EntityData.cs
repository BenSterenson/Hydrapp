using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;


namespace Hydrapp.Client.Helpers
{
    public class EntityData
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
    }
}
