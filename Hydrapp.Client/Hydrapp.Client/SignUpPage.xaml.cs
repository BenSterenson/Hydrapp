using System;
using System.Linq;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent();
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
			var user = new User () {
				Username = usernameEntry.Text,
				Password = passwordEntry.Text,
				Email = emailEntry.Text,
                Weight = weightEntry.Text,
                Height = heightEntry.Text
            };

			// Sign up logic goes here

			var signUpSucceeded = AreDetailsValid (user);
			if (signUpSucceeded)
            {
				var rootPage = Navigation.NavigationStack.FirstOrDefault ();
				if (rootPage != null) {
					App.IsUserLoggedIn = true;
                    //Navigation.InsertPageBefore (new MainPage(), Navigation.NavigationStack.First());
                    //await Navigation.PopToRootAsync ();
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
			}
            else
            {
				messageLabel.Text = "Sign up failed";
			}
		}

		bool AreDetailsValid (User user)
		{
            int n;
			return (!string.IsNullOrWhiteSpace (user.Username) && !string.IsNullOrWhiteSpace (user.Password) && !string.IsNullOrWhiteSpace (user.Email) && user.Email.Contains ("@") &&
                !string.IsNullOrWhiteSpace(user.Weight) && int.TryParse(user.Weight, out n) && !string.IsNullOrWhiteSpace(user.Height) && int.TryParse(user.Weight, out n));
		}
	}
}
