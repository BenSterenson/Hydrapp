using Hydrapp.Client.Services;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public class SignUpPageCS : ContentPage
	{
        private IService AzureDbservice = new AzureDBService();

        Entry usernameEntry, passwordEntry, emailEntry;
		Label messageLabel;

		public SignUpPageCS ()
		{
			messageLabel = new Label ();
			usernameEntry = new Entry {
				Placeholder = "username"	
			};
			passwordEntry = new Entry {
				IsPassword = true
			};
			emailEntry = new Entry ();
			var signUpButton = new Button {
				Text = "Sign Up"
			};
			signUpButton.Clicked += OnSignUpButtonClicked;

			Title = "Sign Up";
			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					new Label { Text = "Username" },
					usernameEntry,
					new Label { Text = "Password" },
					passwordEntry,
					new Label { Text = "Email address" },
					emailEntry,
					signUpButton,
					messageLabel
				}
			};
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
			var user = new User () {
				Username = usernameEntry.Text,
				Password = passwordEntry.Text,
				Email = emailEntry.Text
			};
            
			var signUpSucceeded = AreDetailsValid (user);
            
            // insert to DB
            

            if (signUpSucceeded) {
                
                    await AzureDbservice.addTestItem(user.Username, new DateTime());
                
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
				if (rootPage != null) {
					App.IsUserLoggedIn = true;
					Navigation.InsertPageBefore (new ViewModels.MainPageViewModel(), Navigation.NavigationStack.First());
					await Navigation.PopToRootAsync ();
                    
                }
			} else {
				messageLabel.Text = "Sign up failed";
			}
		}

		bool AreDetailsValid (User user)
		{
            return (!string.IsNullOrWhiteSpace (user.Username) && !string.IsNullOrWhiteSpace (user.Password) && !string.IsNullOrWhiteSpace (user.Email) && user.Email.Contains ("@"));
		}
	}
}
