namespace WhatToDo.Views
{
    using System;
    using DAL.IRepositories;
    using Google.Apis.Tasks.v1.Data;
    using ViewModels;

    /// <summary>
    /// The page for listing all tasks in a list.
    /// </summary>
    /// <seealso cref="WhatToDo.Views.BaseContentPage"/>
    public partial class Tasks : BaseContentPage
    {
        private readonly ITaskRepository taskRepository;
        private readonly TaskList taskList;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasks"/> class.
        /// </summary>
        /// <param name="taskRepository">The task repository.</param>
        /// <param name="taskList">The task list.</param>
        public Tasks(ITaskRepository taskRepository, TaskList taskList)
        {
            this.InitializeComponent();

            this.taskRepository = taskRepository;
            this.taskList = taskList;
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to
        /// the <see cref="T:Xamarin.Forms.Page"/> becoming visible.
        /// </summary>
        /// <remarks>Sets the view model in the binding context.</remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = this.GetTasksViewModel();
        }

        /// <summary>
        /// Called when the back button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync();
        }

        /// <summary>
        /// Gets the tasks view model.
        /// </summary>
        /// <returns>A model containing a list of tasks.</returns>
        private TasksViewModel GetTasksViewModel()
        {
            var viewModel = new TasksViewModel();

            // Get all tasks.
            viewModel.Tasks = System.Threading.Tasks.Task.Run(() => this.taskRepository.GetAllTasksFromTaskList(this.taskList.Id)).Result;
            viewModel.Title = this.taskList.Title;

            return viewModel;
        }
    }
}