using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class UserHydrateLvl : EntityData
    {
        [JsonProperty(PropertyName = "UserId")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "Lvl1Count")]
        public int Lvl1Count { get; set; }
        [JsonProperty(PropertyName = "Lvl1Avg")]
        public long Lvl1Avg { get; set; }
        [JsonProperty(PropertyName = "Lvl2Count")]
        public int Lvl2Count { get; set; }
        [JsonProperty(PropertyName = "Lvl2Avg")]
        public long Lvl2Avg { get; set; }
        [JsonProperty(PropertyName = "Lvl3Count")]
        public int Lvl3Count { get; set; }
        [JsonProperty(PropertyName = "Lvl3Avg")]
        public long Lvl3Avg { get; set; }

        public UserHydrateLvl() { }

        public UserHydrateLvl(int userId)
        {
            UserId = userId;
        }
    }
}
