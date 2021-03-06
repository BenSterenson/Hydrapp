﻿using Hydrapp.Client.Modules;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public class LoginPageCS : ContentPage
	{
		Entry usernameEntry, passwordEntry;
		Label messageLabel;

		public LoginPageCS ()
		{
			var toolbarItem = new ToolbarItem {
				Text = "Sign Up"
			};
			toolbarItem.Clicked += OnSignUpButtonClicked;
			ToolbarItems.Add (toolbarItem);

			messageLabel = new Label ();
			usernameEntry = new Entry {
				Placeholder = "username"	
			};
			passwordEntry = new Entry {
				IsPassword = true
			};
			var loginButton = new Button {
				Text = "Login"
			};
			loginButton.Clicked += OnLoginButtonClicked;

			Title = "Login";
			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					new Label { Text = "Username" },
					usernameEntry,
					new Label { Text = "Password" },
					passwordEntry,
					loginButton,
					messageLabel
				}
			};
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
            Debug.WriteLine("SignUp button clicked");
			await Navigation.PushAsync (new SignUpPageCS ());
		}

		async void OnLoginButtonClicked (object sender, EventArgs e)
		{
			var user = new User {
				Username = usernameEntry.Text,
				Password = passwordEntry.Text
			};

			var isValid = AreCredentialsCorrect (user);
			if (isValid) {
				App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore (new ViewModels.MainPageViewModel(), this);
				await Navigation.PopAsync ();
			} else {
				messageLabel.Text = "Login failed";
				passwordEntry.Text = string.Empty;
			}
		}

		bool AreCredentialsCorrect (User user)
		{
            //TODO check with data base
			return user.userName == Constants.Username && user.password == Constants.Password;
		}
	}
}


