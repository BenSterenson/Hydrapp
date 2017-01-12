using Hydrapp.Client.Modules;
using Hydrapp.Client.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class GroupLoginPage : ContentPage
    {
        private bool createGroup;
        public GroupLoginPage()
        {
            createGroup = true;
            InitializeComponent();
        }

        private async void onToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                buttonLabel.Text = "Create Group";
                createGroup = true;
            }
            else
            {
                buttonLabel.Text = "join Group";
                createGroup = false;
            }
        }      

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (createGroup)
                await DisplayAlert("Group Information", "Group name : " + groupnameEntry.Text + "\nPassword: " + passwordEntry.Text, "OK");
            else
            {
                int userId = checkCredentials(groupnameEntry.Text, passwordEntry.Text);

                if (userId > 0)
                {
                    App.IsUserLoggedIn = true;

                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    messageLabel.Text = "Login failed";
                    passwordEntry.Text = string.Empty;
                }
            }

        }
        private int checkCredentials(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                return -1;
            }
            //return (AzureDbservice.getUserId(userName, password)).Result;

            if (userName == Constants.Username && password == Constants.Password)
                return 1;
            return 0;
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}
