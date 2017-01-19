using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Modules
{
    public class Participant : EntityData, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int rowNumber;
        public User user { get; set; }
        private BandEntry bandEntry;



        public int RowNumber
        {
            get
            {
                return rowNumber;
            }
            set
            {
                rowNumber = value;
                OnPropertyChanged();
            }
        }

        public BandEntry BandEntry
        {
            get
            {
                return bandEntry;
            }
            set
            {
                bandEntry = value;
                OnPropertyChanged();
            }
        }

        //public Group Group { get; set; }
        //public Band Band { get; set; }
        //public Band Band_nfo { get; set; }


        public Participant() { }

        public Participant(User user)
        {
            this.user = user;
        }
        public Participant(int rowNumber, User user)
        {
            this.rowNumber = rowNumber;
            this.user = user;
        }
        public Participant(int rowNumber, User user, BandEntry Entry)
        {
            this.rowNumber = rowNumber;
            this.user = user;
            this.BandEntry = Entry;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
