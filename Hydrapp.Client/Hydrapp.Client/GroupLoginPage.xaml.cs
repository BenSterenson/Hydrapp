using Hydrapp.Client.Modules;
using Hydrapp.Client.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hydrapp.Client
{
	public partial class GroupLoginPage : ContentPage
    {
        private bool createGroup;
        private static IService AzureDbservice = App.AzureDbservice;

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
                await DisplayAlert("Group Information", "Group Id : " + groupIDEntry.Text + "\nPassword: " + passwordEntry.Text, "OK");
                Navigation.InsertPageBefore(new ManageGroupPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                int result = await JoinGroup("userName", groupIDEntry.Text, passwordEntry.Text);

                if (result > 0)
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

        /*Logout*/
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        //Help functions
        private async Task<int> JoinGroup(string userName, string groupID, string groupPassword)
        {
            if (String.IsNullOrEmpty(groupID) || String.IsNullOrEmpty(groupPassword))
            {
                return -1;
            }
            return await (AzureDbservice.joinGroup(userName, groupID, groupPassword));
        }


    }
}
