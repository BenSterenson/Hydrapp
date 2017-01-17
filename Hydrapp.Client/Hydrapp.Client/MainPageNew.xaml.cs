using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydrapp.Client.ViewModels;
using Xamarin.Forms;
using Hydrapp.Client.Services;
using Microsoft.Band.Portable;

namespace Hydrapp.Client
{
    public partial class MainPageNew : ContentPage
    {

        public static int bandSelectedIndex = -1;

        public MainPageNew()
        {
            var toolbarItem = new ToolbarItem
            {
                Text = "Settings"
            };
            toolbarItem.Clicked += OnSettingsButtonClicked;
            ToolbarItems.Add(toolbarItem);

            InitializeComponent();
            MainPageViewModelNew.BandListUpdated += BandListUpdated;

        }

        public void BandListUpdated(object sender, EventArgs e)
        {
            MainPageViewModelNew vm = sender as MainPageViewModelNew;
            if (sender == null)
                return;

            List<BandDeviceInfo> bandlst = vm.BandList;
            BandPicker.Items.Clear();

            if (bandlst != null)
            {
                foreach (var band in bandlst)
                {
                    BandPicker.Items.Add(band.Name);
                }
            }
        }


         public void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (BandPicker.SelectedIndex == -1)
            {
                //Message
            }
            else
            {
                var bandName = BandPicker.Items[BandPicker.SelectedIndex];
                bandSelectedIndex = BandPicker.SelectedIndex;
                //DisplayAlert(bandName, "OK", "OK");
            }
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        /*Settings*/
        async void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingPage());
        }
    }
}
