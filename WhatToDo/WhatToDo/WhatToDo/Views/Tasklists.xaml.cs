﻿namespace WhatToDo.Views
{
    using System;
    using DAL.IRepositories;
    using Google.Apis.Tasks.v1.Data;
    using ViewModels;
    using Xamarin.Forms;

    /// <summary>
    /// The page for listing all task lists.
    /// </summary>
    /// <seealso cref="WhatToDo.Views.BaseContentPage"/>
    public partial class Tasklists : BaseContentPage
    {
        private readonly ITaskListRepository taskListRepository;
        private readonly ITaskRepository taskRepository;
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasklists"/> class.
        /// </summary>
        /// <param name="taskListRepository">The DI injected task list repository.</param>
        /// <param name="taskRepository">The DI injected task repository.</param>
        /// <param name="userRepository">The DI injected user repository.</param>
        public Tasklists(ITaskListRepository taskListRepository, ITaskRepository taskRepository, IUserRepository userRepository)
        {
            this.InitializeComponent();

            this.taskListRepository = taskListRepository;
            this.taskRepository = taskRepository;
            this.userRepository = userRepository;

            if (Device.OS == TargetPlatform.Android)
            {
                // Subscribe the login event for android because the OnAppearing event is not called
                // after PopModalAsync.
                MessagingCenter.Subscribe<App>(this, "LoggedIn", (sender) => this.BindingContext = this.GetTaskListsViewModel());
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

            this.BindingContext = this.GetTaskListsViewModel();
        }

        /// <summary>
        /// Called when a task list item is tapped.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="ItemTappedEventArgs"/> instance containing the event data.
        /// </param>
        private async void OnTaskListItemTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new Tasks(this.taskRepository, (TaskList)e.Item));
        }

        /// <summary>
        /// Called when the menu edit is clicked on the task list page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnEditTaskListClicked(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new EditTaskList(this.taskListRepository, (TaskList)((MenuItem)sender).CommandParameter));
        }

        /// <summary>
        /// Called when the menu delete is clicked on the task list page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnDeleteTaskListClicked(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new DeleteTaskList(this.taskListRepository, (TaskList)((MenuItem)sender).CommandParameter));
        }

        /// <summary>
        /// Called when the new button is clicked on the task list page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnNewTaskListButtonClicked(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new NewTaskList(this.taskListRepository));
        }

        /// <summary>
        /// Called when the logout button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            await this.userRepository.Logout();

            await this.Navigation.PushModalAsync(new Welcome());
        }

        /// <summary>
        /// Gets the task lists view model.
        /// </summary>
        /// <returns>A model containing a list of task lists.</returns>
        private TaskListsViewModel GetTaskListsViewModel()
        {
            var viewModel = new TaskListsViewModel();

            // Get all task lists.
            viewModel.TaskLists = System.Threading.Tasks.Task.Run(() => this.taskListRepository.GetAllTaskLists()).Result;

            return viewModel;
        }
    }
}