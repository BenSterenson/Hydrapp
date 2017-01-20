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
        private int userId = App.User.UserId;
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
                groupLabel.Text = "Group Name";
                buttonLabel.Text = "Create Group";
                groupIdEntry.Placeholder = "Enter Group Name";
                createGroup = true;
            }
            else
            {
                groupLabel.Text = "Group Id";
                buttonLabel.Text = "join Group";
                groupIdEntry.Placeholder = "Enter Group Id";
                createGroup = false;
            }
        }

        /*CreateGroup Toggle */
        async void OnGroupLoginButtonClicked(object sender, EventArgs e)
        {

            if (checkValid(groupIdEntry.Text, passwordEntry.Text) == 1)
            {
                if (createGroup)
                {
                    int GroupId = await AzureDbservice.createGroup(userId, groupIdEntry.Text, passwordEntry.Text);
                    await DisplayAlert("Group Information",
                        "Group Id : " + GroupId + "\nPassword: " + passwordEntry.Text, "OK");
                    App.GroupId = GroupId;
                    Navigation.InsertPageBefore(new ManageGroupPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    int result = await JoinGroup(userId, groupIdEntry.Text, passwordEntry.Text);
                    if (result > 0)
                    {
                        App.IsUserLoggedIn = true;
                        App.GroupId = int.Parse(groupIdEntry.Text);

                        if (result == 2) //user is Admin- Admin mode
                        {
                            Navigation.InsertPageBefore(new ManageGroupPage(), this);
                            await Navigation.PopAsync();
                        }
                        else // user is regular user
                        {
                            Navigation.InsertPageBefore(new MainPageNew(), this);
                            await Navigation.PopAsync();
                        }
                    }
                    else // Invalid credentials
                    {
                        messageLabel.Text = "Login failed. Check credentials";
                        passwordEntry.Text = string.Empty;
                    }
                }
            }
            else // Invalid fields
            {
                messageLabel.Text = "Login failed. Empty fields are not allowed";
                passwordEntry.Text = string.Empty;
            }

        }

        private int checkValid(string text1, string text2)
        {
            if (String.IsNullOrEmpty(text1) || String.IsNullOrEmpty(text2))
            {
                return -1;
            }
            return 1;
        }

        /*Logout*/
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        //Help functions
        private async Task<int> JoinGroup(int userId, string groupIDAsString, string groupPassword)
        {
            int groupId;
            try {
                groupId = int.Parse(groupIDAsString);
            }
            catch
            {
                return -1;
            }
            return await (AzureDbservice.joinGroup(userId, groupId, groupPassword));
        }


    }
}
