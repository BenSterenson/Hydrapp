using Hydrapp.Client.Modules;
using Hydrapp.Client.Services;
using Hydrapp.Client.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class SummaryPage : ContentPage
	{
        private IService AzureDbService = App.AzureDbservice;
        public SummaryPage()
		{
        }

        public SummaryPage(ManageGroupPageViewModel bindingContext)
        {
            InitializeComponent();
            
            this.BindingContext = new SummaryPageViewModel(bindingContext);
            
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            await AzureDbService.updateUserHydrateAvg(App.ActivityId, App.Users);
            await AzureDbService.groupLogout(App.GroupId, App.ActivityId);
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}
