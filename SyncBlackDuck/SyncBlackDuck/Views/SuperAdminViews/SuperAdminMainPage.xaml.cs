﻿using SyncBlackDuck.ViewModel.cAdminViewModel;
using SyncBlackDuck.ViewModel.cSuperAdminViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SyncBlackDuck.Views.SuperAdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuperAdminMainPage : ContentPage
    {
        public SuperAdminMainPage()
        {
            InitializeComponent();
            BindingContext = new SAdminVM(Navigation);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}