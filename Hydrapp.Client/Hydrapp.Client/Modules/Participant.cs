using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class Participant : EntityData
    {
        public User user { get; set; }
        //public Group Group { get; set; }
        //public Band Band { get; set; }
        //public Band Band_nfo { get; set; }


        public Participant() { }

        public Participant(User user)
        {
            this.user = user;

        }
    }
}
