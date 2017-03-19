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
        private string image_temp;
        private string image_fluid;
        public long dehydrateTicks { get; set; }
        public bool notified { get; set; }

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

        public string Image_temp
        {
            get
            {
                return image_temp;
            }
            set
            {
                image_temp = value;
                OnPropertyChanged();
            }
        }
        public string Image_fluid
        {
            get
            {
                return image_fluid;
            }
            set
            {
                image_fluid = value;
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
        public Participant(int rowNumber, User user, BandEntry Entry, long ticks)
        {
            this.rowNumber = rowNumber;
            this.user = user;
            this.bandEntry = Entry;
            this.dehydrateTicks = ticks;
            this.Image_temp = Entry.SkinTemp > 35 ? "overHeat.png" : "normalHeat.png";
            this.Image_fluid = Entry.IsDehydrated ? "dehydration.png" : "normalfluid.png";
        }
        public Participant(int rowNumber, User user, BandEntry Entry, long ticks, ObservableCollection<BandEntry> BandEntryHistory)
        {
            this.rowNumber = rowNumber;
            this.user = user;
            this.bandEntry = Entry;
            this.dehydrateTicks = ticks;
            this.bandEntryHistory = BandEntryHistory;
            this.Image_temp = Entry.SkinTemp > 35 ? "overHeat.png" : "normalHeat.png";
            this.Image_fluid = Entry.IsDehydrated ? "dehydration.png" : "normalfluid.png";
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
