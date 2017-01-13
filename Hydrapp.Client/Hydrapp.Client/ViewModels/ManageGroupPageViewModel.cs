using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Hydrapp.Client.ValueConverters;
using Microsoft.Band.Portable;
using Xamarin.Forms;

using System.Reflection;

using Hydrapp.Client.Modules;


namespace Hydrapp.Client.ViewModels
{
    public class ManageGroupPageViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private string groupName;
        private string numOfParticipants;
        public User User { get; set; }
        private string rowNumber;

        private string avgFluidLoss;
        private string avgHeartRate;
        
        public string userName;


        private List<Participant> participants;

        public ManageGroupPageViewModel()
        {
            groupName = "HydrappGroup";

            participants = new List<Participant> { new Participant(new User("ben", "123", "abc@", 16, 18)), new Participant(new User("michael", "avc", "13@", 16, 18)), new Participant(new User("noam", "123", "abc@", 16, 18)) };
            
            rowNumber = UpdateNumOfParticipants();
            numOfParticipants = UpdateNumOfParticipants();

        }

        

        public string GroupName
        {
            get
            {
                return groupName;
            }

            set
            {
                groupName = value;
                OnPropertyChanged();
            }
        }
        public List<Participant> Participants
        {
            get
            {
                return participants;
            }

            set
            {
                participants = value;
                OnPropertyChanged();
            }
        }

        public string NumOfParticipants
        {
            get
            {
                return numOfParticipants;
            }

            set
            {
                numOfParticipants = value;
                OnPropertyChanged();
            }
        }
        
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        
        public string RowNumber
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

        public string AvgFluidLoss
        {
            get
            {
                return avgFluidLoss;
            }

            set
            {
                avgFluidLoss = value;
                OnPropertyChanged();
            }
        }

        public string AvgHeartRate
        {
            get
            {
                return avgHeartRate;
            }

            set
            {
                avgHeartRate = value;
                OnPropertyChanged();
            }
        }


        string UpdateNumOfParticipants()
        {
            return "Number Of Participants: " + participants.Count().ToString();
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


    }
}
