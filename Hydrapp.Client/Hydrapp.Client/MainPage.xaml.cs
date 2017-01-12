using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hydrapp.Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var toolbarItem = new ToolbarItem
            {
                Text = "Settings"
            };
            toolbarItem.Clicked += OnSettingsButtonClicked;
            ToolbarItems.Add(toolbarItem);

            InitializeComponent();
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
