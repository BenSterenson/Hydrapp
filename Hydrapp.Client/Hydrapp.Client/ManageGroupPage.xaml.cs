using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Hydrapp.Client.Modules;
using Hydrapp.Client.ViewModels;
using System.Collections;
using Hydrapp.Client.Services;

namespace Hydrapp.Client
{
    public partial class ManageGroupPage : ContentPage
    {
        private IService AzureDbService = App.AzureDbservice;

        public ManageGroupPage()
        {
            var toolbarItem = new ToolbarItem
            {
                Text = "Settings"
            };
            toolbarItem.Clicked += OnSettingsButtonClicked;
            ToolbarItems.Add(toolbarItem);

            InitializeComponent();
            
            this.BindingContext = new ManageGroupPageViewModel();

            listView.ItemSelected +=async (object sender, SelectedItemChangedEventArgs e) => {

                var member = e.SelectedItem as Participant;

                if (member == null)
                    return;
                
                await DisplayAlert("ItemSelected", member.user.userName, "Ok");
                await Navigation.PushAsync(new MemberChartPage(member));
            };

            // The ItemSelected is also triggered here.
            

        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            await AzureDbService.groupLogout(App.GroupId, App.ActivityId);
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        /*Settings*/
        async void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingPage());
        }

        async void OnSummaryButtonClicked(object sender, EventArgs e)
        {
            ManageGroupPageViewModel vm = this.BindingContext as ManageGroupPageViewModel;
            await Navigation.PushAsync(new SummaryPage(vm));
        }

    }
}
