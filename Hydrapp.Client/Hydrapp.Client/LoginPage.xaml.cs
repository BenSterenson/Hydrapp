using Hydrapp.Client.Modules;
using Hydrapp.Client.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class LoginPage : ContentPage
	{

        private static IService AzureDbservice = App.AzureDbservice;

        public LoginPage ()
		{
            InitializeComponent();
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new SignUpPage());
		}

		async void OnLoginButtonClicked (object sender, EventArgs e)
		{
			
			int userId = await checkCredentials(usernameEntry.Text, passwordEntry.Text);
			if (userId > 0) {
				App.IsUserLoggedIn = true;
				Navigation.InsertPageBefore (new GroupLoginPage(), this);
				await Navigation.PopAsync ();
			} else {
				messageLabel.Text = "Login failed";
				passwordEntry.Text = string.Empty;
			}
		}

        private async Task<int> checkCredentials(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                return -1;
            }

            return await AzureDbservice.loginUser(userName, password);
        }

    }
}
