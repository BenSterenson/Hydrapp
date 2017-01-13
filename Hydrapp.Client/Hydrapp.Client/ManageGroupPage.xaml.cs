using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Hydrapp.Client.Modules;

namespace Hydrapp.Client
{
    public partial class ManageGroupPage : ContentPage
    {
       
        public ManageGroupPage()
        {
            var toolbarItem = new ToolbarItem
            {
                Text = "Settings"
            };
            toolbarItem.Clicked += OnSettingsButtonClicked;
            ToolbarItems.Add(toolbarItem);

            InitializeComponent();

            listView.ItemSelected +=async (object sender, SelectedItemChangedEventArgs e) => {

                var member = e.SelectedItem as Participant;

                if (member == null)
                    return;

                await DisplayAlert("ItemSelected", member.user.userName, "Ok");
                await Navigation.PushAsync(new SettingPage());
            };

            // The ItemSelected is also triggered here.
            

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
