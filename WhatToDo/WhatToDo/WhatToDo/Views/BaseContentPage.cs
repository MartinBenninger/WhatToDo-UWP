namespace WhatToDo.Views
{
    using Helpers;
    using Xamarin.Forms;

    /// <summary>
    /// The base content page class from which other content pages will derive.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage"/>
    public class BaseContentPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContentPage"/> class.
        /// </summary>
        public BaseContentPage()
        {
            // Check if logged in
            if (!Settings.IsLoggedIn)
            {
                // Redirect to login
                this.Navigation.PushModalAsync(new Welcome());
            }
        }
    }
}