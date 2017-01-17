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
using System.Collections.ObjectModel;

namespace Hydrapp.Client.ViewModels
{
    public class ManageGroupPageViewModel : ContentPage, INotifyPropertyChanged
    {

        private ObservableCollection<Participant> participants = new ObservableCollection<Participant>();
        public ObservableCollection<Participant> Participants
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


        public event PropertyChangedEventHandler PropertyChanged;

        private string groupName;
        private int numOfParticipants;
        
        private string userName;
        private string avgHeartRate;
        private string avgFluidLoss;
        

        public ManageGroupPageViewModel()
        {
            groupName = "HydrappGroup";
            RefreshGroupMembers();
            //Memberleft();
            //numOfParticipants = UpdateNumOfParticipants();

        }

        void RefreshGroupMembers()
        {
            Device.StartTimer(new TimeSpan(0, 0, 0, 2), checkForNewMember);
        }
        void Memberleft()
        {
            Device.StartTimer(new TimeSpan(0, 0, 0, 11), removeMember);
        }

        private bool checkForNewMember()
        {
            // create private field- currentGroupMembers
            // go to DB and check if new members in the group
            // add them here

            Random random = new Random();

            string name = RandomString(random.Next(0, 10));
            string password = RandomString(random.Next(0, 10));
            string email = RandomString(random.Next(0, 10))+ "@gmail.com";
            double height = random.NextDouble() * (2.20 - 1.40) + 1.40;
            double weight = random.NextDouble() * (120 - 55) + 55;

            participants.Add(new Participant(RowCount(), new User(name, password, email, weight, height)));
            NumOfParticipants = participants.Count();
            return true;
        }
        private bool removeMember()
        {
            Random random = new Random();
            participants.RemoveAt(random.Next(0, participants.Count() - 1));
            NumOfParticipants = participants.Count();
            UpdateNumOfRows();
            return true;
        }

        public static string RandomString(int length)
        {
            // TODO REMOVE 
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        void UpdateNumOfRows()
        {
            for (int i = 1; i <= participants.Count(); i++)
            {
                participants[i-1].RowNumber = i;
            }
            OnPropertyChanged();
        }

        int RowCount()
        {
            return participants.Count() + 1;
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


        public int NumOfParticipants
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


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


    }
}
