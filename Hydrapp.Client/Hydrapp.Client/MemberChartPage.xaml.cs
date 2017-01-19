using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Hydrapp.Client.Modules;
using Hydrapp.Client.ViewModels;

namespace Hydrapp.Client
{
    public partial class MemberChartPage : ContentPage
    {
        public MemberChartPage(User user)
        {
            InitializeComponent();

            this.BindingContext = new ChartsViewModel(user);

        }
    }
}