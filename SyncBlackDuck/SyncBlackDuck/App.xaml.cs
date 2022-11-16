﻿using SyncBlackDuck;
using SyncBlackDuck.Services;
using SyncBlackDuck.Views;
using SyncBlackDuck.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SyncBlackDuck
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
