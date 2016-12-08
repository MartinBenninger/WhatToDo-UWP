namespace WhatToDo
{
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

            // For testing and debugging
            Settings.AccessToken = string.Empty;

            // The root page of your application
            this.MainPage = new NavigationPage(new Tasklists(Resolver.Resolve<ITasklistRepository>()));
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
    }
}