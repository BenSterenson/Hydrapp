using Hydrapp.Client.Modules;
using Hydrapp.Client.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class SettingPage : ContentPage
	{
        private IService AzureDbService = App.AzureDbservice;
        public SettingPage()
		{
            InitializeComponent();
		    var user = App.User;
            usernameEntry.Text = user.userName;
            emailEntry.Placeholder = user.email;
            weightEntry.Placeholder = user.weight.ToString();
            heightEntry.Placeholder = user.height.ToString();

        }

        async void OnUpdateButtonClicked(object sender, EventArgs e)
		{
            var user = App.User;
            double n;
            
            
            if (isNotNull(passwordEntry.Text))
            {
                user.password = passwordEntry.Text;
            }
            if (double.TryParse(weightEntry.Text, out n))
            {
                user.weight = Double.Parse(weightEntry.Text);
            }
            if (double.TryParse(heightEntry.Text, out n))
            {
                user.height = Double.Parse(heightEntry.Text);
            }
            
            bool successOnUpdate = await updateUser(user);
            if (successOnUpdate)
            {
                await DisplayAlert("Updated Status", "Successfully updated information", "OK");
                messageLabel.Text = "Successfully updated information";
            }
            else
            {
				messageLabel.Text = "Faild to updated information";
			}
		}

        private async Task<bool> updateUser(User user)
        {
            await AzureDbService.updateUser(user);
            return true;
        }

        bool isNotNull(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

    }
}
