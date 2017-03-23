using Hydrapp.Client.Modules;
using Hydrapp.Client.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Hydrapp.Client
{
    public class SummaryPageViewModel: ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        private int numOfParticipants;
        private int numOfAlerts;
        private int dehydrated_Ratio;

        private string groupName;
        private string activityTime;
        private string activity_Level;
        private string group_performance;
        private ObservableCollection<Participant> participants;
        private double h_FluidLoss_Recorded;
        private string dehydrated_User;
        private string hydrated_User;


        public string Hydrated_User
        {
            get
            {
                return hydrated_User;
            }

            set
            {
                hydrated_User = value;
                OnPropertyChanged();
            }
        }

        public string Dehydrated_User
        {
            get
            {
                return dehydrated_User;
            }

            set
            {
                dehydrated_User = value;
                OnPropertyChanged();
            }
        }

        public double H_FluidLoss_Recorded
        {
            get
            {
                return h_FluidLoss_Recorded;
            }

            set
            {
                h_FluidLoss_Recorded = value;
                OnPropertyChanged();
            }
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

        public string Activity_Level
        {
            get
            {
                return activity_Level;
            }

            set
            {
                activity_Level = value;
                OnPropertyChanged();
            }
        }

        public string Group_performance
        {
            get
            {
                return group_performance;
            }

            set
            {
                group_performance = value;
                OnPropertyChanged();
            }
        }

        public int Dehydrated_Ratio
        {
            get
            {
                return dehydrated_Ratio;
            }

            set
            {
                dehydrated_Ratio = value;
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

        public int NumOfAlerts
        {
            get
            {
                return numOfAlerts;
            }

            set
            {
                numOfAlerts = value;
                OnPropertyChanged();
            }
        }

        public string ActivityTime
        {
            get
            {
                return activityTime;
            }

            set
            {
                activityTime = value;
                OnPropertyChanged();
            }
        }

        private int calc_groupDehydrated_Ratio()
        {
            int dehydrated_count = 0;
            foreach (var par in participants)
            {
                if(par.BandEntry.IsDehydrated == true)
                    dehydrated_count++;
            }
            if (dehydrated_count > 0)
                return (numOfParticipants / dehydrated_count) * 100;
            
            return 0;
        }


        private string calc_group_performance()
        {
            if (numOfAlerts < 10 && dehydrated_Ratio < 10)
                return "Very Good!";
            else
                return "Better drink more!";
        }

        private string convert_activity_to_string(int activity_Level)
        {

            switch (activity_Level)
            {
                case 1:
                    return "Low";
                case 2:
                    return "Medium";
                case 3:
                    return "High";
            }
            return "";
        }

        private void find_fluid_loss_range()
        {
            double maxFluid = 0;
            double minFluid = int.MaxValue;
            bool minUserSet = false;
            string maxDehydrated_User="";
            string maxhydrated_User = "";

            foreach (var par in participants)
            {
                if (par.BandEntry.FluidLoss > maxFluid)
                {
                    maxFluid = par.BandEntry.FluidLoss;
                    maxDehydrated_User = par.user.userName;
                }

                if (par.BandEntry.FluidLoss < minFluid)
                {
                    minFluid = par.BandEntry.FluidLoss;
                    maxhydrated_User = par.user.userName;
                    minUserSet = true;
                }
            }

            if (false == minUserSet)
                minFluid = 0;
            
            dehydrated_User = maxDehydrated_User;
            hydrated_User = maxhydrated_User;
            h_FluidLoss_Recorded = maxFluid;
        }

        public SummaryPageViewModel(string summary_group_name, int summary_activity_Level, int summary_numOfAlerts, TimeSpan summary_activityTime, ObservableCollection<Participant> Participants)
        {
            participants = Participants;
            numOfParticipants = participants.Count;
            groupName = summary_group_name;
            activityTime = summary_activityTime.ToString(@"hh\:mm\:ss");
            activity_Level = convert_activity_to_string(summary_activity_Level);
            numOfAlerts = summary_numOfAlerts;
            dehydrated_Ratio = calc_groupDehydrated_Ratio();
            group_performance = calc_group_performance();
            find_fluid_loss_range();

        }

        public SummaryPageViewModel(ManageGroupPageViewModel bindingContext)
        {
            this.NumOfParticipants = bindingContext.NumOfParticipants;
            this.GroupName = bindingContext.GroupName;
            //this.ActivityTime = bindingContext.stopwatch.Elapsed;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}