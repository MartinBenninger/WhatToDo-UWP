using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToDo.Helpers;
using Xamarin.Auth;
using Xamarin.Forms;

namespace WhatToDo.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            // Check if logged in
            if (!Settings.IsLoggedIn)
            {
                // Redirect to login
                Navigation.PushModalAsync(new Welcome());
            }
        }
    }
}
