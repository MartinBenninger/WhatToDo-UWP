namespace WhatToDo.Views
{
    using System;
    using DAL.IRepositories;
    using Google.Apis.Tasks.v1.Data;
    using ViewModels;

    /// <summary>
    /// The page for editing a task.
    /// </summary>
    /// <seealso cref="WhatToDo.Views.BaseContentPage"/>
    public partial class EditTask : BaseContentPage
    {
        private readonly ITaskRepository taskRepository;
        private readonly TaskList taskList;
        private readonly Task task;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTask"/> class.
        /// </summary>
        /// <param name="taskRepository">The DI injected task repository.</param>
        /// <param name="taskList">The task list.</param>
        /// <param name="task">The task.</param>
        public EditTask(ITaskRepository taskRepository, TaskList taskList, Task task)
        {
            this.InitializeComponent();

            this.taskRepository = taskRepository;
            this.taskList = taskList;
            this.task = task;
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to
        /// the <see cref="T:Xamarin.Forms.Page"/> becoming visible.
        /// </summary>
        /// <remarks>Sets the view model in the binding context.</remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = this.GetEditTaskViewModel();
            this.newTaskName.Focus();
        }

        /// <summary>
        /// Called when the save button is clicked on the edit tasks page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnSaveEditTaskButtonClicked(object sender, EventArgs e)
        {
            await this.SaveNewTaskName();
        }

        /// <summary>
        /// Called when the cancel button is clicked on the edit task page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnCancelEditTaskButtonClicked(object sender, EventArgs e)
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
            await this.SaveNewTaskName();
        }

        /// <summary>
        /// Saves the new name of the task and returns to the tasks page.
        /// </summary>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        private async System.Threading.Tasks.Task SaveNewTaskName()
        {
            this.newTaskName.IsEnabled = false;
            this.task.Title = this.newTaskName.Text;

            await this.taskRepository.UpdateTask(this.taskList, this.task);

            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Gets the edit task view model.
        /// </summary>
        /// <returns>A model containing a task.</returns>
        private EditTaskViewModel GetEditTaskViewModel()
        {
            var viewModel = new EditTaskViewModel();

            viewModel.Task = this.task;

            return viewModel;
        }
    }
}