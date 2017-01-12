using Hydrapp.Client.Modules;
using Hydrapp.Client.Services;
using System;
using System.Linq;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class SettingPage : ContentPage
	{
        private IService AzureDbService = App.AzureDbservice;
        public SettingPage()
		{
            
            InitializeComponent();
            var user = new User() { userName = "abadacaddc", email="afba@gmail.com", height = 5, weight = 10};
            //todo
            //user = readfromDB()

            usernameEntry.Text = user.userName;
            emailEntry.Placeholder = user.email;
            weightEntry.Placeholder = user.weight.ToString();
            heightEntry.Placeholder = user.height.ToString();

        }

        async void OnUpdateButtonClicked(object sender, EventArgs e)
		{
            var user = new User();
            double n;
            
            if (isNotNull(passwordEntry.Text))
            {
                user.password = passwordEntry.Text;
            }
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

            // TODO
            // update user with new values
            bool successOnUpdate = updateUser(user);
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

        private bool updateUser(User user)
        {
            return true;
            //throw new NotImplementedException();
        }

        bool isNotNull(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

    }
}
