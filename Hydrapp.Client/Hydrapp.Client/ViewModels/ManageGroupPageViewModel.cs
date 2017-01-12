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
using Hydrapp.Client.Services;
using System.Collections.ObjectModel;
using Hydrapp.Client.Modules;


namespace Hydrapp.Client.ViewModels
{
    public class ManageGroupPageViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string groupName;
        private string numOfParticipants;
 
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

         public ManageGroupPageViewModel()
        {
            groupName = "HydrappGroup";
            numOfParticipants = UpdateNumOfParticipants();
            
        }

        string UpdateNumOfParticipants()
        {
            return "number Of Participants: " + "18";
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        
    }
}
