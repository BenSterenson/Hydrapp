using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Microsoft.Band.Portable;
using Hydrapp.Client.Services;
using Hydrapp.Client;

namespace Hydrapp.Client
{
    public class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }

        public App()
        {

            if (!IsUserLoggedIn)
            {
                test();
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new Hydrapp.Client.MainPage());
            }
            // The root page of your application
            //MainPage = new MainPage();
        }

        private async void test()
        {
            IService AzureDbservice = new AzureDBService();
            await AzureDbservice.addTestItem("Jimmy the duke", new DateTime(1990,03,28));
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
