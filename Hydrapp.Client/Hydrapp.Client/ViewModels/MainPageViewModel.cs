﻿using System;
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
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Band Setup

        //local band that we will connect to
        private BandDeviceInfo band;

        private BandService bandService;

        // Interface
        
        private string pageTitle;
        private string currentStatus;
        private string connectButtonText;
        private bool connectButtonEnabled;
        //private bool isToggleEnabled;

        // Sensor Stats

        private string readHR;
        private string readHRQuality;
        private string readSkinTemp;
        private string readAmbientLight;
        private string readGSR;
        private string readUV;
        private string readPedometer;
        private string readCalories;
        private string fluidLoss;
        private bool isToggleSkinTemp;
        private bool isToggleAmbientLight;
        private bool isToggleHeartRate;
        private bool isToggleGSR;
        private bool isToggleUV;
        private bool isTogglePedometer;
        private bool isToggleCalories;
        private bool isToggleFluidLoss;

        public bool IsToggleSkinTemp
        {
            get
            {
                return isToggleSkinTemp;
            }

            set
            {
                if (isToggleSkinTemp != value)
                {
                    isToggleSkinTemp = value;
                    OnPropertyChanged();
                    ToggleSkinTemp();
                }
            }
        }

        public bool IsToggleAmbientLight
        {
            get
            {
                return isToggleAmbientLight;
            }

            set
            {
                if (isToggleAmbientLight != value)
                {
                    isToggleAmbientLight = value;
                    OnPropertyChanged();
                    ToggleAmbientLight();
                }
            }
        }

        public bool IsToggleHeartRate
        {
            get
            {
                return isToggleHeartRate;
            }

            set
            {
                if (isToggleHeartRate != value)
                {
                    isToggleHeartRate = value;
                    OnPropertyChanged();
                    ToggleHeartRate();
                }
            }
        }

        public bool IsToggleGSR
        {
            get
            {
                return isToggleGSR;
            }
            set
            {
                if (isToggleGSR != value)
                {
                    isToggleGSR = value;
                    OnPropertyChanged();
                    ToggleGSR();
                }
            }
        }

        public bool IsToggleUV
        {
            get
            {
                return isToggleUV;
            }

            set
            {
                if (isToggleUV != value)
                {
                    isToggleUV = value;
                    OnPropertyChanged();
                    ToggleUV();
                }
            }
        }
        public bool IsTogglePedometer
        {
            get
            {
                return isTogglePedometer;
            }

            set
            {
                if (isTogglePedometer != value)
                {
                    isTogglePedometer = value;
                    OnPropertyChanged();
                    TogglePedometer();
                }
            }
        }

        public bool IsToggleCalories
        {
            get
            {
                return isToggleCalories;
            }

            set
            {
                if (isToggleCalories != value)
                {
                    isToggleCalories = value;
                    OnPropertyChanged();
                    ToggleCalories();
                }
            }
        }
        public bool IsToggleFluidLoss
        {
            get
            {
                return isToggleFluidLoss;
            }

            set
            {
                if (isToggleFluidLoss != value)
                {
                    isToggleFluidLoss = value;
                    OnPropertyChanged();
                    ToggleFluidLoss();
                }
            }
        }
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

        public string CalcFluidLoss
        {
            get
            {
                return fluidLoss;
            }

            set
            {
                fluidLoss = value;
                OnPropertyChanged();
            }
        }
        public MainPageViewModel()
        {
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
            CalcFluidLoss = "Not Active";
            isToggleSkinTemp = false;
            isToggleAmbientLight = false;
            connectButtonEnabled = true;           

            bandService = new BandService();           

            getBands();
        }
        
        private async void getBands()
        {
            
            var bands = await bandService.getBands();
            band = bands.FirstOrDefault();
            if (band == null)
            {
                this.CurrentStatus = "tried but failed";
                return;
            }
            this.ConnectButtonText = "Connect to Band: " + band.Name;
            

           /* try
            {
                // Get the list of Microsoft Bands paired to the phone.
                IBandInfo[] pairedBands = bandService.getBands();

                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {

                }*/
        }

        private async void ConnectToBand()
        {
            this.CurrentStatus = "Connecting...";
            var result = await bandService.ConnectToBand(band);             
            this.CurrentStatus = result;
            this.ConnectButtonEnabled = false;
            bandService.PropertyChanged += BandService_PropertyChanged;
        }

        private async void BandService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                this.ReadSkinTemp = bandService.CurrentSkinTemp;
                this.ReadAmbientLight = bandService.CurrentAmbientLight;
                this.ReadHR = bandService.CurrentHeartRate;
                this.ReadGSR = bandService.CurrentGSR;
                this.ReadUV = bandService.CurrentUV;
                this.ReadPedometer = bandService.CurrentPedometer;
                this.ReadCalories = bandService.CurrentCalories;
                this.CalcFluidLoss = bandService.CurrentFluidLoss;
            });            
        }

        public Command Connect {
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
                return new Command(() =>
                {
                    this.IsToggleSkinTemp = false; isToggleSkinTemp = false; ToggleSkinTemp();
                    this.IsToggleAmbientLight = false; isToggleAmbientLight = false; ToggleAmbientLight();
                    this.IsToggleHeartRate = false; isToggleHeartRate = false; ToggleHeartRate();
                    this.IsToggleGSR = false; isToggleGSR = false; ToggleGSR();
                    this.IsToggleUV = false; isToggleUV = false; ToggleUV();
                    this.IsTogglePedometer = false; isTogglePedometer = false; TogglePedometer();
                    this.IsToggleCalories = false; isToggleCalories = false; ToggleCalories();
                    this.IsToggleFluidLoss = false; isToggleFluidLoss = false; ToggleFluidLoss();
                });
            }
        }
        private bool CheckAvailableSensors()
        {
            if (isToggleSkinTemp && isToggleGSR && isToggleHeartRate && isToggleFluidLoss)
                return true;
            return false;
        }

        // Band Readings
        private async void ToggleSkinTemp()
        {
            if (isToggleSkinTemp)
            {
                await bandService.StartReadingSkinTemp();

                if (CheckAvailableSensors())
                    await bandService.StartReadingFluidLoss();
            }
            else
            {
                try
                {
                    await bandService.StopReadingSkinTemp();
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }
        }
        private async void ToggleAmbientLight()
        {
            if (isToggleAmbientLight)
                await bandService.StartReadingAmbientLight();
            else
            {
                try
                {
                    await bandService.StopReadingAmbientLight();
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }
        }
        private async void ToggleHeartRate()
        {
            if (isToggleHeartRate)
            {
                await bandService.StartReadingHeartRate();

                if (CheckAvailableSensors())
                    await bandService.StartReadingFluidLoss();
            }
            else
            {
                try
                {
                    await bandService.StartReadingHeartRate();
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }
        }
        private async void ToggleGSR()
        {
            if (isToggleGSR)
            {
                await bandService.StartReadingGSR();

                if (CheckAvailableSensors())
                    await bandService.StartReadingFluidLoss();
            }
            else
            {
                try
                {
                    await bandService.StopReadingGSR();
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }
        }
        private async void ToggleUV()
        {
            if (isToggleUV)
                await bandService.StartReadingUV();
            else
            {
                try
                {
                    await bandService.StopReadingUV();
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }
        }
        private async void TogglePedometer()
        {
            if (isTogglePedometer)
                await bandService.StartReadingPedometer();
            else
            {
                try
                {
                    await bandService.StopReadingPedometer();
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }
        }
        private async void ToggleCalories()
        {
            if (isToggleCalories)
                await bandService.StartReadingCalories();
            else
            {
                try
                {
                    await bandService.StopReadingCalories();
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }
        }
        private async void ToggleFluidLoss()
        {

            if (isToggleFluidLoss) {
                this.IsToggleSkinTemp = true;
                isToggleSkinTemp = true;
                ToggleSkinTemp();

                this.IsToggleHeartRate = true;
                isToggleHeartRate = true;
                ToggleHeartRate();

                this.IsToggleGSR = true;
                isToggleGSR = true;
                ToggleGSR();

                await bandService.StartReadingFluidLoss();
            }

            else
            {
                this.IsToggleSkinTemp = false;
                isToggleSkinTemp = false;
                ToggleSkinTemp();

                this.IsToggleHeartRate = false;
                isToggleHeartRate = false;
                ToggleHeartRate();

                this.IsToggleGSR = false;
                isToggleGSR = false;
                ToggleGSR();
               
                try
                {
                    await bandService.StopReadingFluidLoss();
                }
                catch (NullReferenceException)
                {
                    return;
                }
                
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
