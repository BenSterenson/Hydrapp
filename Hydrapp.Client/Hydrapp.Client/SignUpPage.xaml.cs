using System;
using System.Linq;
using Xamarin.Forms;
using Hydrapp.Client.Modules;

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
                userName = usernameEntry.Text,
                password = passwordEntry.Text,
				email = emailEntry.Text,
                weight = parseToDouble(weightEntry.Text),
                height = parseToDouble(heightEntry.Text)
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

        private double parseToDouble(string text)
        {
            try
            {
                return Double.Parse(text);
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        bool AreDetailsValid (User user)
		{
            return true;
      //      double n;
	//		return (!string.IsNullOrWhiteSpace (user.userName) && !string.IsNullOrWhiteSpace (user.password) && !string.IsNullOrWhiteSpace (user.email) && user.email.Contains ("@") &&
       //         !string.IsNullOrWhiteSpace(user.weight) && double.TryParse(user.weight, out n) && !string.IsNullOrWhiteSpace(user.height) && double.TryParse(user.weight, out n));
		}
	}
}
