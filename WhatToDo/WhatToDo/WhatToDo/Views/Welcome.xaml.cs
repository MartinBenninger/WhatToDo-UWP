namespace WhatToDo.Views
{
    using System;
    using Helpers;
    using ViewModels;
    using Xamarin.Forms;

    /// <summary>
    /// The welcome page.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage"/>
    public partial class Welcome : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Welcome"/> class.
        /// </summary>
        public Welcome()
        {
            this.InitializeComponent();

            this.BindingContext = this.GetWelcomeViewModel();
        }

        /// <summary>
        /// Disable the back button. (Don't handle the event.)
        /// </summary>
        /// <returns>True.</returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        /// <summary>
        /// Called when the login button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            await this.Navigation.PushModalAsync(new Login());
        }

        /// <summary>
        /// Gets the welcome view model.
        /// </summary>
        /// <returns>A base view model.</returns>
        private BaseViewModel GetWelcomeViewModel()
        {
            return new BaseViewModel
            {
                Title = "Welcome"
            };
        }
    }
}