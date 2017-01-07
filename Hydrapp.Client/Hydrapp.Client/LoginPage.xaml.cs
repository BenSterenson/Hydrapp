using Hydrapp.Client.Modules;
using System;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class LoginPage : ContentPage
	{
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
			var user = new User {
				userName = usernameEntry.Text,
				password = passwordEntry.Text
			};

			var isValid = AreCredentialsCorrect (user);
			if (isValid) {
				App.IsUserLoggedIn = true;
				Navigation.InsertPageBefore (new MainPage(), this);
				await Navigation.PopAsync ();
			} else {
				messageLabel.Text = "Login failed";
				passwordEntry.Text = string.Empty;
			}
		}

		bool AreCredentialsCorrect (User user)
		{
			return user.userName == Constants.Username && user.password == Constants.Password;
		}
	}
}
