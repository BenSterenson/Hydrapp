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
            var toolbarItem = new ToolbarItem
            {
                Text = "Settings"
            };
            toolbarItem.Clicked += OnSettingsButtonClicked;
            ToolbarItems.Add(toolbarItem);

            createGroup = true;
            InitializeComponent();
        }

        // Buttons

        /*Settings*/
        async void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingPage());
        }

        /*CreateGroup Toggle */
        private void onToggled(object sender, ToggledEventArgs e)
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

        /*CreateGroup Toggle */
        async void OnGroupLoginButtonClicked(object sender, EventArgs e)
        {
            if (createGroup)
            {
                await DisplayAlert("Group Information", "Group name : " + groupnameEntry.Text + "\nPassword: " + passwordEntry.Text, "OK");
                Navigation.InsertPageBefore(new ManageGroupPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                int userId = checkCredentials(groupnameEntry.Text, passwordEntry.Text);

                if (userId > 0)
                {
                    App.IsUserLoggedIn = true;

                    Navigation.InsertPageBefore(new MainPageNew(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    messageLabel.Text = "Login failed";
                    passwordEntry.Text = string.Empty;
                }
            }

        }

        /*Logout*/
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        //Help functions
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


    }
}
