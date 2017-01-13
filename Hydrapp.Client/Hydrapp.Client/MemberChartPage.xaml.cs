﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Hydrapp.Client
{
    public partial class MemberChartPage : ContentPage
    {
        public MemberChartPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when the webview starts navigating. Displays the loading label.
        /// </summary>
        void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            this.labelLoading.IsVisible = true; //display the label when navigating starts
        }

        /// <summary>
        /// Called when the webview finished navigating. Hides the loading label.
        /// </summary>
        void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            this.labelLoading.IsVisible = false; //remove the loading indicator when navigating is finished
        }
    }
}