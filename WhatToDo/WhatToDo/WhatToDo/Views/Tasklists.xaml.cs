namespace WhatToDo.Views
{
    using System;
    using System.Collections.Generic;
    using DAL.IRepositories;
    using Google.Apis.Tasks.v1.Data;
    using Helpers;
    using Models;
    using ViewModels;
    using Xamarin.Forms;

    /// <summary>
    /// The task list page.
    /// </summary>
    /// <seealso cref="WhatToDo.Views.BaseContentPage"/>
    public partial class Tasklists : BaseContentPage
    {
        private readonly ITasklistRepository tasklistRepository;
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasklists"/> class.
        /// </summary>
        /// <param name="tasklistRepository">The DI injected task list repository.</param>
        /// <param name="userRepository">The DI injected user repository.</param>
        public Tasklists(ITasklistRepository tasklistRepository, IUserRepository userRepository)
        {
            this.InitializeComponent();

            this.tasklistRepository = tasklistRepository;
            this.userRepository = userRepository;

            if (Device.OS == TargetPlatform.Android)
            {
                // Subscribe the login event for android because the OnAppearing event is not called
                // after PopModalAsync.
                MessagingCenter.Subscribe<App>(this, "LoggedIn", (sender) => this.BindingContext = this.GetTasklistsViewModel());
            }
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to
        /// the <see cref="T:Xamarin.Forms.Page"/> becoming visible.
        /// </summary>
        /// <remarks>Sets the view model in the binding context.</remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = this.GetTasklistsViewModel();
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs args)
        {
            await this.userRepository.Logout();

            await this.Navigation.PushModalAsync(new Welcome());
        }

        /// <summary>
        /// Gets the task lists view model.
        /// </summary>
        /// <returns>A model containing a list of tasks or empty if the user is not logged in.</returns>
        private TasklistsViewModel GetTasklistsViewModel()
        {
            var viewModel = new TasklistsViewModel();

            // Only set the model data if the user is logged in.
            if (!Settings.IsLoggedIn)
            {
                return viewModel;
            }

            // Get all task lists.
            viewModel.Tasklists = System.Threading.Tasks.Task.Run(() => this.tasklistRepository.GetAllTasklists()).Result;

            return viewModel;
        }
    }
}