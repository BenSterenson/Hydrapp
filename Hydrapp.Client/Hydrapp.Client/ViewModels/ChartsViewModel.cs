﻿using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

using Syncfusion.SfChart.XForms;
using System.Collections.ObjectModel;
using System;

namespace Hydrapp.Client.ViewModels
{
    public class Model
    {
        public string Name { get; set; }

        public double Height { get; set; }
    }

    public class ChartsViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<ChartDataPoint> DehydrationLevel { get; set; }
        

        public ChartsViewModel()
        {

            DehydrationLevel = new ObservableCollection<ChartDataPoint>();
            UpdateDehydrationLevel();
        }
        
        void UpdateDehydrationLevel()
        {
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 300), AddData);
        }

        private bool AddData()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 10);
            int hourRand = random.Next(0, 24);
            int minRand = random.Next(0, 60);
            string randTime = hourRand.ToString() + ":" + minRand.ToString();
            DehydrationLevel.Add(new ChartDataPoint(randTime, randomNumber));
            return true;
        }

    }
}