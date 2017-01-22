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
using System.Threading;

using System.Reflection;

using Hydrapp.Client.Modules;
using System.Collections.ObjectModel;
using Hydrapp.Client.Services;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Hydrapp.Client.ViewModels
{
    public class ManageGroupPageViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IService AzureDbService = App.AzureDbservice;
        private List<int> currentMembersList = new List<int>();
        private ObservableCollection<Participant> participants = new ObservableCollection<Participant>();
        private bool noticed = false;
        private string groupName;
        private int numOfParticipants;
        private Color backgroundColor;

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

        public ManageGroupPageViewModel()
        {
            groupName = "HydrappGroup";

            RefreshGroupMembers();
            updateMembersTimer();
            //Memberleft();

        }
        void RefreshGroupMembers()
        {
            Device.StartTimer(new TimeSpan(0, 0, 0, 5), checkForNewMember);
        }
        void updateMembersTimer()
        {
            Device.StartTimer(new TimeSpan(0, 0, 0, 10), updateValues);
        }
        
        private bool checkForNewMember()
        {
            addNewMembers();

            /*Generate random users*/
            //GenerateaddNewMembers();
            return true;
        }
        private bool updateValues()
        {
            update();

            /*Generate random values for users*/
            //updateVal();
            return true;
        }
        private async void update()
        {
            int numOfMembers = Participants.Count();
            for (int i = 0; i < numOfMembers; i++)
            {
                BandEntry latest = await AzureDbService.getLatestBandEntryForUser(participants[i].user.UserId);
                var member = participants[i];

                // check if not null and latest date > members date
                if (latest != null && (DateTime.Compare(member.BandEntry.TimeStamp, latest.TimeStamp) < 0))
                {
                    member.BandEntry = latest;
                    Participants[i] = new Participant(member.RowNumber, member.user, member.BandEntry, member.BandEntryHistory);
                    if (latest.IsDehydrated && noticed == false)
                    {
                        await DisplayAlert("Dehydration Alert", member.user.userName + " is dehydrated!!", "Ok");
                        noticed = true;
                    }
                }
            }
        }
 
        private async void addNewMembers()
        {
            List<User> membersToAdd = await AzureDbService.getNewMembers(currentMembersList, App.GroupId);
            foreach (var user in membersToAdd)
            {
                BandEntry latest = await AzureDbService.getLatestBandEntryForUser(user.UserId);
                if (latest != null)
                {
                    participants.Add(new Participant(RowCount(), user, latest));
                    currentMembersList.Add(user.UserId);
                }
            }
            NumOfParticipants = participants.Count();
        }

        private bool removeMember()
        {
            Random random = new Random();
            participants.RemoveAt(random.Next(0, participants.Count() - 1));
            NumOfParticipants = participants.Count();
            UpdateNumOfRows();
            return true;
        }

        void UpdateNumOfRows()
        {
            for (int i = 1; i <= participants.Count(); i++)
            {
                participants[i - 1].RowNumber = i;
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

        public new Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }

            set
            {
                backgroundColor = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }

        /// <summary>
        /// ************************************************
        /// ************************************************
        /// ************************************************
        /// ************************************************
        /// FUNCTIONS FOR DEBUG
        /// ************************************************
        /// ************************************************
        /// ************************************************
        /// ************************************************
        /// </summary>
        /// 


        private void GenerateaddNewMembers()
        {
            // TODO REMOVE 
            if (participants.Count() < 9)
            {
                User user = GenerateRandUser();
                BandEntry latest = GenerateBandEntry();
                participants.Add(new Participant(RowCount(), user, latest));
            }
            NumOfParticipants = participants.Count();
        }
        private void updateVal()
        {
            // TODO REMOVE 
            int numofusers = Participants.Count();
            for (int i = 0; i < numofusers; i++)
            {
                BandEntry latest = GenerateBandEntry();
                var member = participants[i];
                member.BandEntry = latest;
                Participants[i] = new Participant(member.RowNumber, member.user, member.BandEntry, member.BandEntryHistory);
            }
        }

        public BandEntry GenerateBandEntry()
        {
            // TODO REMOVE 

            Random random = new Random();
            int hearRate = random.Next(70, 200);
            double skinTemp = random.NextDouble() * (37 - 28) + 28;
            double hear = random.NextDouble() * (100 - 50) + 50;
            double fluidloss = random.NextDouble() * (10 - 0) + 0;

            return new BandEntry(DateTime.Now, 32, 3, 3, 3, skinTemp, 0, hearRate, 0, 0, 0, fluidloss, false);
        }
        public User GenerateRandUser()
        {
            // TODO REMOVE 
            Random random = new Random();
            int nameLen = random.Next(1, 10);
            double height = random.NextDouble() * (2.20 - 1.50) + 1.50;
            double weight = random.NextDouble() * (100 - 50) + 50;

            return new User(RandomString(nameLen), "abc", "abc@", weight, height);
        }

        public static string RandomString(int length)
        {
            // TODO REMOVE 
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// ************************************************
        /// ************************************************
        /// ************************************************
        /// ************************************************
        /// END FUNCTIONS FOR DEBUG
        /// ************************************************
        /// ************************************************
        /// ************************************************
        /// ************************************************
        /// </summary>

    }
}
