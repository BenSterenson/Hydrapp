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



namespace Hydrapp.Client.ViewModels
{

    public class MainPageViewModelNew : ContentPage, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public static event EventHandler<EventArgs> BandListUpdated;
        

        private void OnBandListUpdated(EventArgs e)
        {
            if (BandListUpdated != null)
                BandListUpdated(this, e);
        }

        // Band Setup

        //local band that we will connect to
        private BandDeviceInfo band;
        private BandService bandService;

        private List<BandDeviceInfo> bandList;
        public List<BandDeviceInfo> BandList
        {
            get { return bandList; }
            set
            {
                bandList = value;
                OnBandListUpdated(null);
            }
        }

        // Interface
        private string pageTitle;
        private string currentStatus;
        private string connectButtonText;
        private bool connectButtonEnabled;
        private static int bandSelectedIndex;


        // Sensor Stats
        private string readHR;
        private string readHRQuality;
        private string readSkinTemp;
        private string readAmbientLight;
        private string readGSR;
        private string readUV;
        private string readPedometer;
        private string readCalories;
        private string readFluidLoss;


        public string PageTitle
        {
            get
            {
                return pageTitle;
            }

            set
            {
                pageTitle = value;
                OnPropertyChanged();
            }
        }

        public string CurrentStatus
        {
            get
            {
                return currentStatus;
            }

            set
            {
                currentStatus = value;
                OnPropertyChanged();
            }
        }

        public string ConnectButtonText
        {
            get
            {
                return connectButtonText;
            }

            set
            {
                connectButtonText = value;
                OnPropertyChanged();
            }
        }

        public bool ConnectButtonEnabled
        {
            get
            {
                return connectButtonEnabled;
            }

            set
            {
                connectButtonEnabled = value;
                OnPropertyChanged();
                OnPropertyChanged("IsToggleEnabled");
            }
        }

        public bool IsToggleEnabled
        {
            get
            {
                this.ConnectButtonText = "Connected to Band: " + band.Name;
                return !connectButtonEnabled;
            }
        }

        public string ReadHR
        {
            get
            {
                return readHR;
            }

            set
            {
                readHR = value;
                OnPropertyChanged();
            }
        }

        public string ReadHRQuality
        {
            get
            {
                return readHRQuality;
            }

            set
            {
                readHRQuality = value;
                OnPropertyChanged();
            }
        }
        public string ReadSkinTemp
        {
            get
            {
                return readSkinTemp;
            }

            set
            {
                readSkinTemp = value;
                OnPropertyChanged();
            }
        }

        public string ReadAmbientLight
        {
            get
            {
                return readAmbientLight;
            }

            set
            {
                readAmbientLight = value;
                OnPropertyChanged();
            }
        }
        public string ReadGSR
        {
            get
            {
                return readGSR;
            }

            set
            {
                readGSR = value;
                OnPropertyChanged();
            }
        }
        public string ReadUV
        {
            get
            {
                return readUV;
            }

            set
            {
                readUV = value;
                OnPropertyChanged();
            }
        }

        public string ReadPedometer
        {
            get
            {
                return readPedometer;
            }

            set
            {
                readPedometer = value;
                OnPropertyChanged();
            }
        }

        public string ReadCalories
        {
            get
            {
                return readCalories;
            }

            set
            {
                readCalories = value;
                OnPropertyChanged();
            }
        }

        public string ReadFluidLoss
        {
            get
            {
                return readFluidLoss;
            }

            set
            {
                readFluidLoss = value;
                OnPropertyChanged();
            }
        }
        public MainPageViewModelNew()
        {
            //TODO set page title as group name 
            pageTitle = "Hydrapp";
            currentStatus = "Current Status";
            connectButtonText = "Connect to Band";

            readHR = "Not Active";
            readSkinTemp = "Not Active";
            readAmbientLight = "Not Active"; ;
            readGSR = "Not Active";
            readUV = "Not Active";
            readPedometer = "Not Active";
            readCalories = "Not Active";
            readFluidLoss = "Not Active";

            connectButtonEnabled = true;
            bandService = new BandService();

            getBandList();
            //getBands();

        }

        public async void getBandList()
        {
            var bands = await bandService.getBands();
            BandList = bands.ToList();
            if (BandList == null)
            {
                this.CurrentStatus = "No bands found";
            }
        }

        private async void getBands()
        {

            var bands = await bandService.getBands();
            band = bands.FirstOrDefault();
            if (band == null)
            {
                this.CurrentStatus = "No bands found";
                return;
            }
            this.ConnectButtonText = "Connect to Band: " + band.Name;

        }

        private async void ConnectToBand()
        {
            this.CurrentStatus = "Connecting...";

            int index = MainPageNew.bandSelectedIndex;
            band = BandList[index];

            var result = await bandService.ConnectToBand(band);
            if (result)
            {
                this.CurrentStatus = "Connected to band : " + band.Name;
                this.ConnectButtonEnabled = false;
                bandService.PropertyChanged += BandService_PropertyChanged;

                // Band Readings
                await bandService.StartReadingSkinTemp();
                await bandService.StartReadingHeartRate();
                await bandService.StartReadingGSR();
                await bandService.StartReadingUV();
                await bandService.StartReadingAmbientLight();
                await bandService.StartReadingPedometer();
                await bandService.StartReadingCalories();
                //await bandService.StartReadingFluidLoss();

                InserToDB();// TODO
                return;
            }
            this.CurrentStatus = "Failed to connect : " + band.Name;
        }

        void InserToDB()
        {
            // send data every 5 sec
            Device.StartTimer(new TimeSpan(0, 0, 0, 5), SendDataToCloud);
        }

        private bool SendDataToCloud()
        {
          //TODO
            return true;
        }

        private async void BandService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                this.ReadSkinTemp = bandService.CurrentSkinTemp;
                this.ReadHR = bandService.CurrentHeartRate;
                this.ReadGSR = bandService.CurrentGSR;
                this.ReadUV = bandService.CurrentUV;
                this.ReadAmbientLight = bandService.CurrentAmbientLight;
                this.ReadPedometer = bandService.CurrentPedometer;
                this.ReadCalories = bandService.CurrentCalories;
                //this.ReadFluidLoss = bandService.CurrentFluidLoss;
            });
        }

        public Command Connect
        {
            get
            {
                return new Command(() =>
                {
                    ConnectToBand();
                });
            }
        }

        public Command StopAllSensors
        {
            get
            {
                return new Command(async () =>
                {
                    await bandService.StopReadingSkinTemp();
                });
            }
        }



        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
