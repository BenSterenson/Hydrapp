using Hydrapp.Client.Modules;
using Hydrapp.Client.Services;
using System;
using System.Linq;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class SignUpPage : ContentPage
	{
        
        private IService AzureDbService = App.AzureDbservice;
        public SignUpPage ()
		{
			InitializeComponent();
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{

            var signUpSucceeded = validateInfo();
			// Sign up logic goes here

			if (signUpSucceeded)
            {
                var user = new User()
                {
                    userName = usernameEntry.Text,
                    password = passwordEntry.Text,
                    email = emailEntry.Text,
                    weight = Double.Parse(weightEntry.Text),
                    height = Double.Parse(heightEntry.Text)
                };

                var rootPage = Navigation.NavigationStack.FirstOrDefault ();
				if (rootPage != null) {
					App.IsUserLoggedIn = true;
                    await AzureDbService.addUser(user);
                    App.User = user;
                    // user have UserId

                    //Navigation.InsertPageBefore (new MainPage(), Navigation.NavigationStack.First());
                    //await Navigation.PopToRootAsync ();
                    Navigation.InsertPageBefore(new GroupLoginPage(), this);
                    await Navigation.PopAsync();
                }
			}
            else
            {
				messageLabel.Text = "Sign up failed";
			}
		}

		/*bool AreDetailsValid (User user)
		{
            int n;
			return (!string.IsNullOrWhiteSpace (user.userName) && !string.IsNullOrWhiteSpace (user.password) && !string.IsNullOrWhiteSpace (user.email) && user.email.Contains ("@") &&
                !string.IsNullOrWhiteSpace(user.Weight) && int.TryParse(user.Weight, out n) && !string.IsNullOrWhiteSpace(user.Height) && int.TryParse(user.Weight, out n));
		}*/
        bool isNotNull(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        bool validateInfo()
        {
            double n;

            if (double.TryParse(weightEntry.Text, out n) && double.TryParse(heightEntry.Text, out n))
            {
                if (isNotNull(usernameEntry.Text) && isNotNull(passwordEntry.Text) && isNotNull(emailEntry.Text))
                {
                    if (emailEntry.Text.Contains("@"))
                        return true;
                }
            }
            return false;
        }
    }
}
