﻿using Hydrapp.Client.Modules;
using Hydrapp.Client.ViewModels;
using Plugin.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Hydrapp.Client
{
    public class SummaryPageViewModel: ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        private int numOfParticipants;
        private int numOfAlerts;
        private int dehydrated_Percentage;

        private string groupId;
        private string activityTime;
        private string activity_Level;
        private string group_performance;
        private ObservableCollection<Participant> participants;
        private double h_FluidLoss_Recorded;
        private double l_FluidLoss_Recorded;
        private double d_FluidLoss_Recorded;
        private string dehydrated_User;
        private string hydrated_User;
        public ICommand OnSendMailButtonClicked
        {
            get; private set;
        }

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

        public double L_FluidLoss_Recorded
        {
            get
            {
                return l_FluidLoss_Recorded;
            }

            set
            {
                l_FluidLoss_Recorded = value;
                OnPropertyChanged();
            }
        }
        public double D_FluidLoss_Recorded
        {
            get
            {
                return d_FluidLoss_Recorded;
            }

            set
            {
                d_FluidLoss_Recorded = value;
                OnPropertyChanged();
            }
        }


        public string GroupId
        {
            get
            {
                return groupId;
            }

            set
            {
                groupId = value;
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

        public int Dehydrated_Percentage
        {
            get
            {
                return dehydrated_Percentage;
            }

            set
            {
                dehydrated_Percentage = value;
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

        private int calc_groupDehydrated_Percentage()
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
            if (numOfAlerts < 10 && dehydrated_Percentage == 0)
                return "Very Good!";
            if (numOfAlerts < 10 && dehydrated_Percentage < 10)
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
            l_FluidLoss_Recorded = minFluid;
        }

        public SummaryPageViewModel(int summary_activity_Level, int summary_numOfAlerts, TimeSpan summary_activityTime, ObservableCollection<Participant> Participants)
        {
            participants = Participants;
            numOfParticipants = participants.Count;
            GroupId = App.GroupId.ToString();
            activityTime = summary_activityTime.ToString(@"hh\:mm\:ss");
            Activity_Level = convert_activity_to_string(summary_activity_Level);
            numOfAlerts = summary_numOfAlerts;
            dehydrated_Percentage = calc_groupDehydrated_Percentage();
            group_performance = calc_group_performance();
            find_fluid_loss_range();
            d_FluidLoss_Recorded = h_FluidLoss_Recorded - l_FluidLoss_Recorded;
            OnSendMailButtonClicked = new Command(SendMailCommand);

        }


        void SendMailCommand()
        {
            var emailMessenger = MessagingPlugin.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                String timeStamp = DateTime.Now.ToString("dd.MM.yyyy");
                string mailContent = "WorkOut Summary\n";
                mailContent += "\n\n";
                mailContent += "Group ID:\t" + GroupId + "\n"; 
                mailContent += "Number Of Participants:\t" + NumOfParticipants.ToString() + "\n";
                mailContent += "Activity Level:\t" + Activity_Level + "\n";
                mailContent += "Activity Time:\t" + ActivityTime + "\n";
                mailContent += "Number Of Alerts:\t" + NumOfAlerts.ToString() + "\n"; 
                mailContent += "Dehydrated Members Percentage:\t" + Dehydrated_Percentage.ToString() + "%" + "\n";
                mailContent += "Least Hydrated User:\t" + Dehydrated_User + "\n";
                mailContent += "Highest Fluid Loss Recorded:\t" + H_FluidLoss_Recorded.ToString() + "%" + "\n";
                mailContent += "Most Hydrated User:\t" + Hydrated_User + "\n";
                mailContent += "Lowest Fluid Loss Recorded:\t" + L_FluidLoss_Recorded.ToString() + "%" + "\n";
                mailContent += "Highest Lowest Record Difference:\t" + D_FluidLoss_Recorded.ToString() + "%" + "\n";
                mailContent += "Group Performance:\t" + Group_performance + "\n";
                mailContent += "\n\n";

                var email = new EmailMessageBuilder()

                  .To(App.User.email.ToString())
                  .Subject("Hydrapp Workout Summary " + timeStamp)
                  .Body(mailContent)
                  .Build();

                emailMessenger.SendEmail(email);

            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}