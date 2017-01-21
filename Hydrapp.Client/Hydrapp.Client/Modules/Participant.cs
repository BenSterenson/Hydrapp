using Hydrapp.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private int rowNumber;
        public User user { get; set; }
        private ObservableCollection<BandEntry> bandEntryHistory = new ObservableCollection<BandEntry>();
        private BandEntry bandEntry;
        private string image_src = "checkmark.png";

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

        public string Image_src
        {
            get
            {
                return image_src;
            }
            set
            {
                image_src = value;
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
                bandEntryHistory.Add(bandEntry);
            }
        }
        public ObservableCollection<BandEntry> BandEntryHistory
        {
            get
            {
                return bandEntryHistory;
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
            this.bandEntry = Entry;
        }
        public Participant(int rowNumber, User user, BandEntry Entry, ObservableCollection<BandEntry> BandEntryHistory)
        {
            this.rowNumber = rowNumber;
            this.user = user;
            this.bandEntry = Entry;
            this.bandEntryHistory = BandEntryHistory;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
