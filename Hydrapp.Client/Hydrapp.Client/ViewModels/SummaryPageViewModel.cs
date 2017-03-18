using Hydrapp.Client.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Hydrapp.Client
{
    public class SummaryPageViewModel: ContentPage, INotifyPropertyChanged
    {

        private int NumOfParticipants;
        private string GroupName;
        private TimeSpan ActivityTime;

        public SummaryPageViewModel()
        {
        }

        public SummaryPageViewModel(ManageGroupPageViewModel bindingContext)
        {
            this.NumOfParticipants = bindingContext.NumOfParticipants;
            this.GroupName = bindingContext.GroupName;
            this.ActivityTime = bindingContext.stopwatch.Elapsed;
        }
    }
}