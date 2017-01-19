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
			
			User user = await checkCredentials(usernameEntry.Text, passwordEntry.Text);
			if (user != null) {
				App.IsUserLoggedIn = true;
                App.User = user;
				Navigation.InsertPageBefore (new GroupLoginPage(), this);
				await Navigation.PopAsync ();
			} else {
				messageLabel.Text = "Login failed";
				passwordEntry.Text = string.Empty;
			}
		}

        private async Task<User> checkCredentials(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                return null;
            }

            return await AzureDbservice.loginUser(userName, password);
        }

    }
}
