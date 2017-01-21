using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

using Syncfusion.SfChart.XForms;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hydrapp.Client.Modules;
using Hydrapp.Client.Services;

namespace Hydrapp.Client.ViewModels
{

    public class ChartsViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static IService AzureDbservice = App.AzureDbservice;
        private List<BandEntry> historicData;
        private int counter = 0;
        private User CurrentUser;
        private Participant CurrentParticipant;
        private BandEntry currentEntry;
        public ObservableCollection<ChartDataPoint> DehydrationLevel { get; set; }


        public ChartsViewModel(Participant member)
        {
            CurrentParticipant = member;
            CurrentUser = member.user;

            //GetHistoricData();
            DehydrationLevel = new ObservableCollection<ChartDataPoint>();
            UpdateDehydrationLevel();
        }

        private async void GetHistoricData()
        {
            historicData = await AzureDbservice.getBandEntriesForUser(CurrentUser);
        }

        void UpdateDehydrationLevel()
        {
            //Device.StartTimer(new TimeSpan(0, 0, 0, 0, 300), AddData);
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 300), UseMemberInfo);
        }

        private bool UseMemberInfo()
        {
            if (counter < CurrentParticipant.BandEntryHistory.Count)
            {
                currentEntry = CurrentParticipant.BandEntryHistory[counter];
                counter++;
                DehydrationLevel.Add(new ChartDataPoint(currentEntry.TimeStamp, currentEntry.FluidLoss));
            }
            return true;
        }


        private bool AddData()
        {
            if (counter < historicData.Count)
            {
                currentEntry = historicData.ElementAt(counter);
                counter++;
            }
            else
            {
                setLatestData();
            }
            DehydrationLevel.Add(new ChartDataPoint(currentEntry.TimeStamp, currentEntry.FluidLoss));
            return true;
        }

        private async void setLatestData()
        {
            currentEntry = await AzureDbservice.getLatestBandEntryForUser(CurrentUser.UserId);
        }
    }
}