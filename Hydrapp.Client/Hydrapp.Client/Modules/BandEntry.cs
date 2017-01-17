using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class BandEntry :EntityData
    {
        [JsonProperty(PropertyName = "TimeStamp")]
        public DateTime TimeStamp { get; set; }
        [JsonProperty(PropertyName = "GroupId")]
        public int GroupId { get; set; }
        [JsonProperty(PropertyName = "UserId")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "BandId")]
        public int BandId { get; set; }
        [JsonProperty(PropertyName = "GSR")]
        public int GSR { get; set; }
        [JsonProperty(PropertyName = "SkinTemp")]
        public double SkinTemp { get; set; }
        [JsonProperty(PropertyName = "Light")]
        public int Light { get; set; }
        [JsonProperty(PropertyName = "HeartRate")]
        public int HeartRate { get; set; }
        [JsonProperty(PropertyName = "UV")]
        public int UV { get; set; }
        [JsonProperty(PropertyName = "Calories")]
        public int Calories { get; set; }
        [JsonProperty(PropertyName = "Steps")]
        public int Steps { get; set; }
        [JsonProperty(PropertyName = "FluidLoss")]
        public double FluidLoss { get; set; }
        [JsonProperty(PropertyName = "IsDehydrated")]
        public bool IsDehydrated { get; set; }
    }
}
