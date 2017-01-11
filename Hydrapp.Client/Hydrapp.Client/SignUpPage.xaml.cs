using Hydrapp.Client.Modules;
using Hydrapp.Client.Services;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class SignUpPage : ContentPage
	{
        private IService AzureDbservice = App.AzureDbservice;
        public SignUpPage ()
		{
			InitializeComponent();
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
            var user = new User() {
                userName = usernameEntry.Text,
                password = passwordEntry.Text,
                email = emailEntry.Text,
                height = 20,
                weight = 123
			};

            // Sign up logic goes here


            if (AreDetailsValid(user) == false)
            {
                messageLabel.Text = "Sign up failed, not valid";
            }
            else
            {
                try
                {
                    await AzureDbservice.addUser(user);

                    var rootPage = Navigation.NavigationStack.FirstOrDefault();
                    if (rootPage != null)
                    {
                        App.IsUserLoggedIn = true;
                        //Navigation.InsertPageBefore (new MainPage(), Navigation.NavigationStack.First());
                        //await Navigation.PopToRootAsync ();
                        Navigation.InsertPageBefore(new MainPage(), this);
                        await Navigation.PopAsync();
                    }
                }
                catch (Exception execption)
                {
                    Debug.WriteLine("SignUp Failed!", execption);
                }
            }
            
		}

		bool AreDetailsValid (User user)
		{
			return (!string.IsNullOrWhiteSpace (user.userName) && !string.IsNullOrWhiteSpace (user.password) && !string.IsNullOrWhiteSpace (user.email) && user.email.Contains ("@"));
		}
	}
}
