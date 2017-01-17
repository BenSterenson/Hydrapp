using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class GroupMember: EntityData
    {
        [JsonProperty(PropertyName = "UserId")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "GroupId")]
        public int GroupId { get; set; }
        [JsonProperty(PropertyName = "Admin")]
        public bool Admin { get; set; }

        public GroupMember() { }

        public GroupMember(int UserId, int GroupId, bool Admin)
        {
            this.UserId = UserId;
            this.GroupId = GroupId;
            this.Admin = Admin;
        }
    }
}
