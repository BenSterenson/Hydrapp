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
        [JsonProperty(PropertyName = "ActivityId")]
        public int ActivityId { get; set; }
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

        public BandEntry(DateTime timeStamp, int activityId, int groupId, int userId, int bandId, int gsr, double skinTemp,
        int light, int heartRate, int UV, int calories, int steps, double floidLoss, bool IsDehydrated)
        {
            this.TimeStamp = timeStamp;
            this.ActivityId = activityId;
            this.GroupId = groupId;
            this.UserId = userId;
            this.BandId = bandId;
            this.GSR = gsr;
            this.SkinTemp = skinTemp;
            this.Light = light;
            this.HeartRate = heartRate;
            this.UV = UV;
            this.Calories = calories;
            this.Steps = steps;
            this.FluidLoss = floidLoss;
            this.IsDehydrated = IsDehydrated;
        }
    }
}
