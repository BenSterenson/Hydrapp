using Hydrapp.Client.ViewModels;
using System;
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
            int dehydrated=1;
            return (numOfParticipants / dehydrated) * 100;
        }


        private string calc_group_performance()
        {
            if (numOfAlerts < 10 && dehydrated_Ratio < 10)
                return "Very Good!";
            else
                return "better drink more!";
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

    public SummaryPageViewModel(string summary_group_name, int summary_numOfParticipants, int summary_activity_Level, int summary_numOfAlerts)
        {
            numOfParticipants = summary_numOfParticipants;
            groupName = summary_group_name;
            activityTime = new TimeSpan(10, 20, 30).ToString();
            activity_Level = convert_activity_to_string(summary_activity_Level);
            numOfAlerts = summary_numOfAlerts;
            dehydrated_Ratio = calc_groupDehydrated_Ratio();
            group_performance = calc_group_performance();
            

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