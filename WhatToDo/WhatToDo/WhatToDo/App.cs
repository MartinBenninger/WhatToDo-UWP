using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatToDo.Helpers;
using WhatToDo.Views;
using Xamarin.Forms;

namespace WhatToDo
{
    public class App : Application
    {
        public App()
        {
            // For testing and debugging
            Settings.AccessToken = string.Empty;

            // The root page of your application
            MainPage = new NavigationPage(new Tasklists());
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
