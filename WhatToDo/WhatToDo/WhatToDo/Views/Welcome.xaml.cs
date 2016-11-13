using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToDo.Helpers;
using WhatToDo.ViewModels;
using Xamarin.Forms;

namespace WhatToDo.Views
{
    public partial class Welcome : ContentPage
    {
        public Welcome()
        {
            InitializeComponent();

            BindingContext = GetWelcomeViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            Settings.AccessToken = "token";
            await Navigation.PopModalAsync();

            if (Device.OS == TargetPlatform.Android)
            {
                // Send the login event for android because the OnAppearing event is not called after PopModalAsync.
                MessagingCenter.Send<Welcome>(this, "LoggedIn");
            }
        }

        private BaseViewModel GetWelcomeViewModel()
        {
            return new BaseViewModel
            {
                Title = "Welcome"
            };
        }
    }
}
