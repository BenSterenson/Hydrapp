using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class Band :EntityData
    {
        [JsonProperty(PropertyName = "UserId")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "BandId")]
        public int BandId { get; set; }
        [JsonProperty(PropertyName = "BandName")]
        public string BandName { get; set; }

        public Band(int userId, int bandId, string bandName)
        {
            this.UserId = userId;
            this.BandId = bandId;
            this.BandName = bandName;
        }
    }

    
}
