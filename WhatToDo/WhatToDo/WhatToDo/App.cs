namespace WhatToDo
{
    using System.Threading.Tasks;
    using DAL.IRepositories;
    using Helpers;
    using Views;
    using Xamarin.Forms;
    using XLabs.Ioc;

    /// <summary>
    /// The main application class.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Application"/>
    public class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            // Set up the inversion of control bindings.
            if (!Resolver.IsSet)
            {
                IocConfig.SetIoc();
            }

            NavigationHelper.Current.NavigateLoginSuccess = this.LoginSuccess;
            NavigationHelper.Current.NavigateLoginFailure = this.LoginFailure;

            // The root page of your application
            this.MainPage = new NavigationPage(new Tasklists(Resolver.Resolve<ITaskListRepository>(), Resolver.Resolve<ITaskRepository>(), Resolver.Resolve<IUserRepository>()));
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application starts.
        /// </summary>
        /// <remarks>To be added.</remarks>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application
        /// enters the sleeping state.
        /// </summary>
        /// <remarks>To be added.</remarks>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application
        /// resumes from a sleeping state.
        /// </summary>
        /// <remarks>To be added.</remarks>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// Method to call if the login was successful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task LoginSuccess()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                // Send the login event for android because the OnAppearing event is not called after PopModalAsync.
                MessagingCenter.Send<App>(this, "LoggedIn");
            }

            await this.MainPage.Navigation.PopModalAsync();
            await this.MainPage.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Method to call if the login failed or was canceled.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task LoginFailure()
        {
            await this.MainPage.Navigation.PopModalAsync();
        }
    }
}