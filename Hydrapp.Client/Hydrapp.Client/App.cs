using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Microsoft.Band.Portable;
using Hydrapp.Client.Services;
using Hydrapp.Client;
using System.Diagnostics;
using Hydrapp.Client.Modules;

namespace Hydrapp.Client
{
    public class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public static User User;
        public static int GroupId;
        public static int ActivityId;
        public static int ActivityLvl;
        public static List<int> Users = new List<int>();
        public static IService AzureDbservice = new AzureDBService(); 
        public App()
        {

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new Hydrapp.Client.MainPageNew());
            }
            // The root page of your application
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
