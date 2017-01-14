namespace WhatToDo.Views
{
    using System;
    using DAL.IRepositories;
    using Google.Apis.Tasks.v1.Data;
    using ViewModels;

    /// <summary>
    /// The page for creating a new task.
    /// </summary>
    /// <seealso cref="WhatToDo.Views.BaseContentPage"/>
    public partial class NewTask : BaseContentPage
    {
        private readonly ITaskRepository taskRepository;
        private readonly TaskList taskList;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewTask"/> class.
        /// </summary>
        /// <param name="taskRepository">The task repository.</param>
        /// <param name="taskList">The task list.</param>
        public NewTask(ITaskRepository taskRepository, TaskList taskList)
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

            this.BindingContext = this.GetNewTaskViewModel();
            this.newTaskName.Focus();
        }

        /// <summary>
        /// Called when the save button is clicked on the new task page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnSaveNewTaskButtonClicked(object sender, EventArgs e)
        {
            await this.SaveNewTask();
        }

        /// <summary>
        /// Called when the cancel button is clicked on the new task page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnCancelNewTaskButtonClicked(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Called when the user has ended input by pressing the return key on the keyboard.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnNewTaskNameCompleted(object sender, EventArgs e)
        {
            await this.SaveNewTask();
        }

        /// <summary>
        /// Saves the new name of the task and returns to the task page.
        /// </summary>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        private async System.Threading.Tasks.Task SaveNewTask()
        {
            this.newTaskName.IsEnabled = false;
            var task = new Task();
            task.Title = this.newTaskName.Text;

            await this.taskRepository.InsertTask(this.taskList, task);

            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Gets the new task view model.
        /// </summary>
        /// <returns>A model for creating a new task.</returns>
        private BaseViewModel GetNewTaskViewModel()
        {
            var viewModel = new BaseViewModel();

            viewModel.Title = "New task";

            return viewModel;
        }
    }
}