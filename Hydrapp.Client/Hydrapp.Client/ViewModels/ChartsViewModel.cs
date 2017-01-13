using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;




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
        

        public List<Model> Data { get; set; }

        public ChartsViewModel()
        {

            Data = new List<Model>()
            {
                new Model { Name = "David", Height = 180 },
                new Model { Name = "Michael", Height = 170 },
                new Model { Name = "Steve", Height = 160 },
                new Model { Name = "Joel", Height = 182 }
            };
        }
    }
}