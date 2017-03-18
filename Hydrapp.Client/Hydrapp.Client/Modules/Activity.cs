using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class Activity: EntityData
    {
        [JsonProperty(PropertyName = "ActivityId")]
        public int ActivityId { get; set; }
        [JsonProperty(PropertyName = "GroupId")]
        public int GroupId { get; set; }
        [JsonProperty(PropertyName = "ActivityLvl")]
        public int ActivityLvl { get; set; }
        [JsonProperty(PropertyName = "ActivityTime")]
        public DateTime ActivityTime { get; set; }
        [JsonProperty(PropertyName = "Done")]
        public int Done { get; set; }
        public Activity() { }
        
    }
}
